using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InsureThatAPI.Models
{
    public class FarmPolicyBurglary
    {
    }

    public class FPBurglary
    {
        public int CustomerId { get; set; }

        public CoverYourPropOptionFP CoverYourPropOptionFPObj { get; set; }

        public FarmBuildingExcCoolRoomsFP FarmBuildingExCoolRoomsFPObj { get; set; }

        public CoolRoomsFP CoolRoomsFPObj { get; set; }

        public FarmFencingFP FarmFencingFPObj { get; set; }

        public OtherFarmStructuresFP OtherFarmStructuresFPObj { get; set; }

        public FarmContentsFP FarmContentsFPObj { get; set; }   

        public HailNettingStoredFP HailNettingStoredFPObj { get; set; }

        public UnspecifiedMachineryFP UnspecifiedMachineryFPObj { get; set; }

        public SpecifiedItemsOver5KFP SpecifiedItemsOver5KFPObj { get; set; }

        public ExcessFPBurglary ExcessFPBurglaryObj { get; set; }

        public FPCoverTheftFSAndFC OptFPCoverTheftFSAndFCObj { get; set; }

        public FPCoverTheftFarmMachinery OptFPCoverTheftFarmMachineryObj { get; set; }

        public FPPortalableItemsOpt OptFPPortalableItemsOptObj { get; set; }
    }

    public class CoverYourPropOptionFP
    {
        public string Cover { get; set; }
        public int EiId { get; set; }
    }

    public class FarmBuildingExcCoolRoomsFP
    {
        public string FarmBuildingExcCoolRooms { get; set; }
        public int EiId { get; set; }
    }

    public class CoolRoomsFP
    {
        public string CoolRooms { get; set; }
        public int EiId { get; set; }
    }

    public class FarmFencingFP
    {
        public string FarmFencing { get; set; }
        public int EiId { get; set; }
    }

    public class OtherFarmStructuresFP
    {
        public string OtherFarmStructures { get; set; }
        public int EiId { get; set; }
    }

    public class FarmContentsFP
    {
        public string FarmContents { get; set; }
        public int EiId { get; set; }
    }

    public class HailNettingStoredFP
    {
        public string HailNettingStored { get; set; }
        public int EiId { get; set; }
    }

    public class UnspecifiedMachineryFP
    {
        public string UnspecifiedMachinery { get; set; }
        public int EiId { get; set; }
    }

    public class SpecifiedItemsOver5KFP
    {
        public string SpecifiedItemOver5K { get; set; }
        public int EiId { get; set; }
    }

    public class ExcessFPBurglary
    {
        public IEnumerable<SelectListItem> ExcessList { get; set; }
        public string Excess { get; set; }
        public int EiId { get; set; }
    }

    public class FPCoverTheftFSAndFC
    {
        public string CoverTheftFSandFC { get; set; }
        public int EiId { get; set; }
    }

    public class FPCoverTheftFarmMachinery
    {
        public string CoverTheftFarmMachinery { get; set; }
        public int EiId { get; set; }
    }

    public class FPPortalableItemsOpt
    {
        public string PortalbleItemsOpt { get; set; }
        public int EiId { get; set; }
    }
}