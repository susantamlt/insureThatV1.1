using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InsureThatAPI.Models;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net;

namespace InsureThatAPI.CommonMethods
{
    public class LogInDetailsClass
    {

        #region 
        public static bool Validate(string mainresponse, string privatekey)
        {

            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create
                ("https://www.google.com/recaptcha/api/siteverify?secret=" +
                privatekey + "&response=" + mainresponse);

                WebResponse response = req.GetResponse();

                using (StreamReader readStream = new StreamReader(response.GetResponseStream()))
                {
                    string jsonResponse = readStream.ReadToEnd();

                    JsonResponseObject jobj = JsonConvert.DeserializeObject<JsonResponseObject>(jsonResponse);

                    return jobj.success;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        public class JsonResponseObject
        {
            public bool success { get; set; }
            [JsonProperty("error-codes")]
            public List<string> errorcodes { get; set; }
        }
        #endregion

        #region Fields

        //salt value - any string
        private static string saltValue = "T1msSaLt13crypt0";
        // Init Vector - must be 16 ASCII bytes
        private static string initVector = "1Nit1aLiz316ByT3";
        // Hash Algorithm to be used (SHA1 and MD5 are acceptable - SHA1 is more secure)
        private static string hashAlgorithm = "SHA1";
        // Number of iterations to create a password
        private static int iterations = 2;
        // Default Bit Rate - 256 bit encryption
        private static int keySize = 256;
        // Padding Mode
        private static PaddingMode padMode = PaddingMode.ISO10126;

        #endregion
        #region Public Methods

        public string APIkeyEncrypt(string Plaintext, string EncryptedKey)
        {
            string APIKey = string.Empty;
            APIKey = Encrypt(Plaintext, EncryptedKey);
            return APIKey;

        }



        public static string Encrypt(string plainText, string encryptionKey)
        {
            try
            {
                // Convert Strings into Byte Arrays
                byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
                byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);
                // Convert Original String to Byte Array
                byte[] plainTextBytes = Encoding.ASCII.GetBytes(plainText);
                // Create a password from which the key will be generated
                PasswordDeriveBytes password = new PasswordDeriveBytes(encryptionKey, saltValueBytes, hashAlgorithm, iterations);
                // Use Password to generate pseudo-random bytes for the encryption key. Specify the size of the key in bytes (instead of bits)
                byte[] keyBytes = password.GetBytes(keySize / 8);
                // Create Encryption object (uninitialized)
                RijndaelManaged symetricKey = new RijndaelManaged();
                symetricKey.Padding = padMode;
                // Set encrpyt mode to Cipher Block Chaining
                symetricKey.Mode = CipherMode.CBC;
                // Generate encryptor from the existing key bytes and vector
                ICryptoTransform encryptor = symetricKey.CreateEncryptor(keyBytes, initVectorBytes);
                // Define Memory Stream which holds encrypted data
                MemoryStream memoryStream = new MemoryStream();
                // Define Cryptography Stream
                CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
                // Start Encrypting
                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                // End Encrypting
                cryptoStream.FlushFinalBlock();
                // Convert data from a mem stream to byte array
                byte[] cipherTextBytes = memoryStream.ToArray();
                // Close Streams
                memoryStream.Close();
                cryptoStream.Close();
                // Convert Encrypted Data into a base64 encoded string
                string cipherText = Convert.ToBase64String(cipherTextBytes);
                // Return encrypted string
                return cipherText;
            }
            catch (Exception xp)
            {
                throw;
            }
        }

        public static string Decrypt(string cipherText, string encryptionKey)
        {
            try
            {
                // Convert Strings into Byte Arrays
                byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
                byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);
                // Convert Cipher String to Byte Array
                byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
                // Create a password from which the key will be generated
                PasswordDeriveBytes password = new PasswordDeriveBytes(encryptionKey, saltValueBytes, hashAlgorithm, iterations);
                // Use Password to generate pseudo-random bytes for the encryption key. Specify the size of the key in bytes (instead of bits)
                byte[] keyBytes = password.GetBytes(keySize / 8);
                // Create Encryption object (uninitialized)
                RijndaelManaged symetricKey = new RijndaelManaged();
                ///Encryption Padding mode
                symetricKey.Padding = padMode;
                // Set encrpyt mode to Cipher Block Chaining
                symetricKey.Mode = CipherMode.CBC;
                // Generate decryptor from the existing key bytes and vector
                ICryptoTransform decryptor = symetricKey.CreateDecryptor(keyBytes, initVectorBytes);
                // Define Memory Stream which holds encrypted data
                MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
                // Define Cryptography Stream
                CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
                // Allocate buffer long enough to hold string
                byte[] plainTextBytes = new byte[cipherText.Length];
                // Start Decrypting
                int plainTextByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                // Close Streams
                memoryStream.Close();
                cryptoStream.Close();
                // Convert Encrypted Data into a base64 encoded string
                string plainText = Encoding.UTF8.GetString(plainTextBytes, 0, plainTextByteCount);
                // Return encrypted string
                return plainText;
            }
            catch (Exception xp)
            {
                return "BadEncryptionProcess";
            }
        }

        #endregion
        #region Get Customer/Call Center Login details from database
        public async Task<LoginDetailsRef> GetLogInDetailsPage(string UserName, string Password)
        {
            string strEncrypt = string.Empty;
            string strDecrypt = string.Empty;
            string PlainTextEncrpted = string.Empty;
            string loginKey = string.Empty;
            int IyId = 9262;
            string EncrptForLogin = String.Format("{0:ddddyyyyMMdd}", DateTime.UtcNow);
            EncrptForLogin = "Tuesday20171212";
            PlainTextEncrpted = IyId + "|" + UserName + "|InsureThatDirect";
            loginKey = Encrypt(PlainTextEncrpted, EncrptForLogin);
            LoginDetailsRef loginDetailsref = new LoginDetailsRef();
            LogInDetails logindetailsmodel = new LogInDetails();
            MasterDataEntities db = new MasterDataEntities();
            loginDetailsref.ErrorMessage = new List<string>();
            try
            {

                HttpClient hclient = new HttpClient();
                hclient.BaseAddress = new Uri("https://api.insurethat.com.au/");
                hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                loginKey = loginKey.Replace("+", "%2B");
                HttpResponseMessage Res = await hclient.GetAsync("Api/Login?loginKey=" + loginKey + "");//change controller name and field name
                                                                                                        //   LogInDetails loginmodel = new LogInDetails();
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list // EncryptedPassword
                    logindetailsmodel = JsonConvert.DeserializeObject<LogInDetails>(EmpResponse);
                    strEncrypt = Encrypt(Password, "TimsFirstEncryptionKey");//encrypt password method
                                                                             //   strDecrypt = Decrypt(strEncrypt, "TimsFirstEncryptionKey");//decrypt password method

                    if (logindetailsmodel.EncryptedPassword != null && strEncrypt == logindetailsmodel.EncryptedPassword.ToString())
                    {
                        loginDetailsref.Status = "Success";
                        logindetailsmodel.UserName = UserName;
                        logindetailsmodel.Password = Password;
                        loginDetailsref.LogInData = logindetailsmodel;
                        // Session["apiKey"] = logindetailsmodel.apiKey;
                        HttpContext.Current.Session["apiKey"] = logindetailsmodel.apiKey;
                        HttpContext.Current.Session["UserName"] = logindetailsmodel.UserName;
                    }
                    else
                    {
                        loginDetailsref.Status = "Failure";
                        loginDetailsref.ErrorMessage.Add("Password is not valid.");
                    }
                }
            }
            catch (Exception xp)
            {
                loginDetailsref.Status = "Failure";
                loginDetailsref.ErrorMessage.Add(xp.Message);
            }
            finally
            {

            }
            return loginDetailsref;
        }

        #endregion

    }
}