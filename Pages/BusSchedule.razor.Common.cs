using System.Data;

namespace Wasm_BusSchedule.Pages
{
    public partial class BusSchedule
    {
  
        /// <summary>
        /// 全部路線List
        /// </summary>
        public List<C_Route> Route_Data = new List<C_Route>();

        /// <summary>
        /// 目前路線搜尋條件
        /// </summary>
        public string SearchCondition_RouteName { get; set; }

        /// <summary>
        /// 目前使用Token
        /// </summary>
        public C_TOKEN CurrentTokenInfo { get; set; } = new C_TOKEN();

        /// <summary>
        /// 是否點擊單筆路線
        /// </summary>
        public bool _businfoClicked { get; set; } = false;

        /// <summary>
        /// 目前選擇的路線資料(單筆)
        /// </summary>
        public C_Route CurrentRouteData { get; set; }



        /// <summary>
        /// Token Post需要的參數Dictionary，都是固定的，[client_id]、[client_secret]是TDX申請的金鑰
        /// </summary>
        Dictionary<string, string> DicFor_TokenPostData = new Dictionary<string, string>()
        {
            { "grant_type" , "client_credentials" },
            { "client_id" , "goatwindow82-3dd7131f-dafb-4def" },
            { "client_secret" , "cc1cc546-c394-4b71-89d8-59f0845928cb" },
        };

        




      




    }
}
