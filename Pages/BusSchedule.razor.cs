using System.Net;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace Wasm_BusSchedule.Pages
{

    public partial class BusSchedule{
        
        protected override async Task OnInitializedAsync()
        {

            //取得新Token
            var newToken = await GetNewToken();

            if (newToken != null)
            {
                CurrentTokenInfo._Token_Type = newToken.token_type;
                CurrentTokenInfo._AccessToken = newToken.access_token;

                await LoadProcess_Route();
            }


        }

        /// <summary>
        /// 讀取路線資料過程
        /// </summary>
        private async Task LoadProcess_Route()
        {
            //「讀取全部公車路線」的回傳值
            (HttpStatusCode, List<C_Route>) routeValue = await LoadData_Route();

            HttpStatusCode routeStatus = routeValue.Item1;
            List<C_Route> routeData = routeValue.Item2;

            //依照回傳狀態進行判斷
            await StatusProccess(routeStatus, routeData, false);
        }




        /// <summary>
        /// 依回傳狀態判斷是否顯示資料到畫面上
        /// </summary>
        private async Task StatusProccess(HttpStatusCode targetStatus, List<C_Route> targetRouteData, bool isRetry, int retryCount = 10)
        {
            switch (targetStatus)
            {
                case HttpStatusCode.OK:
                    {
                        Route_Data = targetRouteData;
                    }
                    break;
            }
        }


        /// <summary>
        /// 取得新Token，回傳新Token字串
        /// </summary>
        private async Task<C_POST_TOKEN> GetNewToken()
        {
            string tokencontent = await GetTokenContent(DicFor_TokenPostData);
            var tokendata = JsonConvert.DeserializeObject<C_POST_TOKEN>(tokencontent);
            return tokendata;
        }

        /// <summary>
        /// POST取得AccessToken
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private async Task<string> GetTokenContent(Dictionary<string, string> data)
        {
            string requestURL = "https://tdx.transportdata.tw/auth/realms/TDXConnect/protocol/openid-connect/token";

            using (HttpClient httpclient = new HttpClient())
            {
                httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
                HttpResponseMessage response = await httpclient.PostAsync(requestURL, new FormUrlEncodedContent(data));
                string result = await response.Content.ReadAsStringAsync();
                return result;
            }
        }


        /// <summary>
        /// 讀取全部路線資料
        /// </summary>
        /// <returns></returns>
        private async Task<(HttpStatusCode, List<C_Route>)> LoadData_Route()
        {
            //讀取API取得json回傳資料
            (bool, HttpStatusCode, string) content = await GetJsonContent_Route();

            bool _isSuccess = content.Item1;
            HttpStatusCode statuscode = content.Item2;
            string jsoncontent = content.Item3;


            var emptyList = new List<C_Route>();
            //  Tuple<HttpStatusCode, List<C_Route>> ret = Tuple.Create(statuscode, emptyList);
            (HttpStatusCode, List<C_Route>) ret = (statuscode, emptyList);
            //API資料取得成功，就講JSON格式資料轉為List
            if (_isSuccess)
            {
                var contentList = JsonConvert.DeserializeObject<List<C_Route>>(jsoncontent);
                ret = (statuscode, contentList);
            }

            return ret;

        }

        /// <summary>
        /// 讀取全部路線資料
        /// </summary>
        /// <returns></returns>
        private async Task<(bool, HttpStatusCode, string)> GetJsonContent_Route()
        {
            string routeURL = "https://tdx.transportdata.tw/api/basic/v2/Bus/Route/City/Taipei?%24format=JSON";

            using (HttpClient httpclient = new HttpClient())
            {
                bool _responseStatus = false;
                HttpStatusCode statuscode = HttpStatusCode.Unused;
                string responseContent = "";
                try
                {
                    httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(CurrentTokenInfo._Token_Type, CurrentTokenInfo._AccessToken);
                    // httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(TokenInfo[0]._Token_Type, TokenInfo[0]._AccessToken);
                    httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage HttpResponse_Route = await httpclient.GetAsync(routeURL);

                    _responseStatus = HttpResponse_Route.IsSuccessStatusCode;
                    statuscode = HttpResponse_Route.StatusCode;

                    if (_responseStatus)
                    {
                        responseContent = await HttpResponse_Route.Content.ReadAsStringAsync();
                    }

                }
                catch (Exception ex)
                {
                    var error = ex.Message;
                }

                return (_responseStatus, statuscode, responseContent);
            }
        }


        //根據條件，從母體List取得篩選過後的資料
        //TODO : 目前支援一個條件，未來希望可以支援多個條件，類。模糊搜尋
        private List<C_Route> GetData_SearchByRoute(string routeName, List<C_Route> motherList)
        {
            List<C_Route> ret = new List<C_Route>();

            //拿routeName去motherList裡找資料
            ret = motherList.FindAll(x => x.RouteName.Zh_tw.Contains(routeName) || x.RouteName.En.Contains(routeName));

            return ret;
        }




    }
}