using System.Diagnostics;


[DebuggerDisplay("token_type={token_type},access_token={access_token}")]
/// <summary>
/// Class - 對應DBTable[TOKEN]
/// </summary>
public class C_TOKEN
{
    public Int64 _SN { get; set; }
    public string _Token_Type { get; set; }
    public string _AccessToken { get; set; }
    public DateTime _Update_Time { get; set; }

}

/// <summary>
/// Class - 對應POST取回來的TOKEN
/// </summary>
public class C_POST_TOKEN
{
    public string access_token { get; set; }
    public int expires_in { get; set; }
    public int refresh_expires_in { get; set; }
    public string token_type { get; set; }
    public int notbeforepolicy { get; set; }
    public string scope { get; set; }
}





[DebuggerDisplay("中文路線名={RouteName.Zh_tw}")]
/// <summary>
/// 公車路線Class
/// </summary>
public class C_Route
{
    public string RouteUID { get; set; }
    public string RouteID { get; set; }
    public bool HasSubRoutes { get; set; }
    public Operator[] Operators { get; set; }
    public string AuthorityID { get; set; }
    public string ProviderID { get; set; }
    public Subroute[] SubRoutes { get; set; }
    public int BusRouteType { get; set; }
    public Routename RouteName { get; set; }
    /// <summary>
    /// 起點站(中文)
    /// </summary>
    public string DepartureStopNameZh { get; set; }
    /// <summary>
    /// 起點站(英文)
    /// </summary>
    public string DepartureStopNameEn { get; set; }
    /// <summary>
    /// 終點站(中文)
    /// </summary>
    public string DestinationStopNameZh { get; set; }
    /// <summary>
    /// 終點站(英文)
    /// </summary>
    public string DestinationStopNameEn { get; set; }
    public string TicketPriceDescriptionZh { get; set; }
    public string TicketPriceDescriptionEn { get; set; }
    public string RouteMapImageUrl { get; set; }
    public string City { get; set; }
    public string CityCode { get; set; }
    public DateTime UpdateTime { get; set; }
    public int VersionID { get; set; }
    public string FareBufferZoneDescriptionZh { get; set; }
    public string FareBufferZoneDescriptionEn { get; set; }
}






public class Routename
{
    public string Zh_tw { get; set; }
    public string En { get; set; }
}



public class Operator
{
    public string OperatorID { get; set; }
    public Operatorname OperatorName { get; set; }
    public string OperatorCode { get; set; }
    public string OperatorNo { get; set; }
}

public class Operatorname
{
    public string Zh_tw { get; set; }
    public string En { get; set; }
}

public class Subroute
{
    public string SubRouteUID { get; set; }
    public string SubRouteID { get; set; }
    public string[] OperatorIDs { get; set; }
    public Subroutename SubRouteName { get; set; }
    public int Direction { get; set; }
    public string FirstBusTime { get; set; }
    public string LastBusTime { get; set; }
    public string HolidayFirstBusTime { get; set; }
    public string HolidayLastBusTime { get; set; }
}

public class Subroutename
{
    public string Zh_tw { get; set; }
    public string En { get; set; }
}















