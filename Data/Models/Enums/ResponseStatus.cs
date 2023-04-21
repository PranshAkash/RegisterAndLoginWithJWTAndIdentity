using System.ComponentModel.DataAnnotations;

namespace Data.Models.Enums
{
    /// <summary>
    /// ENUM 
    /// </summary>
    /// <remarks>
    ///<ul>  
    ///<li>-1 = Failed</li>
    ///<li>1 = Success</li>
    ///<li>2 = Pending</li>
    ///<li>3 = Token Expired</li>
    ///  </ul>
    /// </remarks>
    public enum ResponseStatus
    {
        Failed = -1,
        Success = 1,
        Pending = 2,       
        [Display(Name = "Token Expired")]
        TokenExpired = 3,
        warning = 4
    }
}
