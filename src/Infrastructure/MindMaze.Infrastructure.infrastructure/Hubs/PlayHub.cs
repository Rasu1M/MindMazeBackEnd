using Microsoft.AspNetCore.SignalR;
using MindMaze.Core.Application.Interfaces.IGenericRepository;
using MindMaze.Core.Domain;
using MindMaze.Core.Domain.ResponseObjects.QuestionsResponse;
using MindMaze.Infrastructure.infrastructure.Data;
using System.Globalization;
using System.Threading;

namespace MindMaze.Infrastructure.infrastructure.Hubs
{
    public sealed class PlayHub : Hub
    {
        //to find an opponent we need others' id
        private static List<string> _clients = new List<string>();

        private static List<UserInfo> _onlineClients = new List<UserInfo>();

        private static Dictionary<string, info> tokentouserstatus = new Dictionary<string, info>();

        private static Dictionary<string, string> CollectionToIdToken = new Dictionary<string, string>();


        private const int DefaultQuestionCount = 12;


        private readonly ApplicationDBContext _dbContext;

        public PlayHub(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }



        private object _lock = new object();

        public override Task OnConnectedAsync()
        {

            //our front end doesnt work because of that. i send first this empty calls
            Clients.Client(Context.ConnectionId).SendAsync("OpponentNotFound");

            Clients.Client(Context.ConnectionId).SendAsync("AAASASSASD", "DNEEME");

            Clients.Client(Context.ConnectionId).SendAsync("DeleteOpponentInfo");
        

        Clients.Client(Context.ConnectionId)
                .SendAsync("TakeAnswer", null);

            Clients.Client(Context.ConnectionId)
                .SendAsync("WinForLeaving");

            Clients.Client(Context.ConnectionId).SendAsync("GetOnlineUsers", null);

            Clients.Client(Context.ConnectionId).SendAsync("OpponentFound", null, null);


            return base.OnConnectedAsync();
        }


        public override async Task OnDisconnectedAsync(Exception exception)
        {
            lock (_lock)
            {


                var idtoken = CollectionToIdToken[Context.ConnectionId];


                if (tokentouserstatus[idtoken].Opponent_TokenID != null)
                {

                    Clients.Client(tokentouserstatus[tokentouserstatus[idtoken].Opponent_TokenID].Connection_ID)
                        .SendAsync("WinForLeaving");

                    //set null enemy's opponenttokenid if there is an enemy
                    tokentouserstatus[tokentouserstatus[idtoken].Opponent_TokenID] = null;


                }


                tokentouserstatus[idtoken].IsConnected = false;
                tokentouserstatus[idtoken].WantToplay = false;
                tokentouserstatus[idtoken].Disconnnected_Time = DateTime.Now;


                CollectionToIdToken[Context.ConnectionId] = null;

            }

            await base.OnDisconnectedAsync(exception);

        }

        // Game
        public async Task FindOpponent()
        {
            // null connection

           

            var playeridtoken = CollectionToIdToken[Context.ConnectionId];



            tokentouserstatus[playeridtoken].WantToplay = true;

            string opponentidtoken;



            //TODO We can use lock if we want to avoid all duplication possibilities

            if (_clients.Count < 20)
            {
                Thread.Sleep(2000);
            }




            lock (_lock)
            {
                if (tokentouserstatus[playeridtoken].WantToplay == false)
                {
                    return;
                }

              

                opponentidtoken = _clients.FirstOrDefault(x => x != playeridtoken && tokentouserstatus[x].WantToplay == true);

              


                if (opponentidtoken == null)
                {
                    tokentouserstatus[playeridtoken].WantToplay = false;
                   
                    Clients.Client(Context.ConnectionId).SendAsync("OpponentNotFound");
                    return;
                }



                tokentouserstatus[opponentidtoken].WantToplay = false;
                tokentouserstatus[playeridtoken].WantToplay = false;

            }





            tokentouserstatus[playeridtoken].Opponent_TokenID = opponentidtoken;

            tokentouserstatus[opponentidtoken].Opponent_TokenID = playeridtoken;

          


            var opponentConnectionID = tokentouserstatus[opponentidtoken].Connection_ID;

         



            var playerinfo = _onlineClients.FirstOrDefault(x => x.IDToken == playeridtoken);

            var opponentinfo = _onlineClients.FirstOrDefault(x => x.IDToken == opponentidtoken);

           


            var questions = GetQuestions();


            Clients.Client(Context.ConnectionId).SendAsync("OpponentFound", opponentinfo, questions);

            Clients.Client(opponentConnectionID).SendAsync("OpponentFound", playerinfo, questions);



            //check doubles

        }

        //change answer type to char
        public async Task SendMyAnswer(string OpponentTokenID, string answer)
        {
            //we can change and use stored opponenttokenid
            Clients.Client(tokentouserstatus[OpponentTokenID]?.Connection_ID)
                .SendAsync("TakeAnswer", answer);
        }

        public async Task BeOnline(UserInfo userinfo)
        {
           

            if (tokentouserstatus.ContainsKey(userinfo.IDToken))
            {
                tokentouserstatus[userinfo.IDToken].Connection_ID = Context.ConnectionId;
                //check playing

                tokentouserstatus[userinfo.IDToken].IsConnected = true;

                CollectionToIdToken.Add(Context.ConnectionId, userinfo.IDToken);

                Clients.Client(Context.ConnectionId).SendAsync("AAASASSASD", "BeOnline1");
            }

            

            else
            {

                lock (_lock)
                {

                    Clients.Client(Context.ConnectionId).SendAsync("AAASASSASD", "BeOnline2");

                    _clients.Add(userinfo.IDToken);

                    _onlineClients.Add(userinfo);

                    CollectionToIdToken.Add(Context.ConnectionId, userinfo.IDToken);

                    tokentouserstatus.Add(userinfo.IDToken, new info()
                    {
                        Connection_ID = Context.ConnectionId,
                        WantToplay = false,
                        IsConnected = true
                    });

                    Clients.Client(Context.ConnectionId).SendAsync("AAASASSASD", "BeOnline3");
                }
            }

        }

        public async Task GiveUp(string OpponentTokenID)
        {
            Clients.Client(tokentouserstatus[OpponentTokenID].Connection_ID)
                .SendAsync("WinForLeaving");
        }

        public async Task CancelGameRequest()
        {
            var tokenid = CollectionToIdToken[Context.ConnectionId];

            tokentouserstatus[tokenid].WantToplay = false;
        }


        //public async Task SendGameResult(){}

        //Infos


        public async Task GetOnlineUsers()
        {
            Clients.Client(Context.ConnectionId).SendAsync("GetOnlineUsers", _onlineClients);
        }

        public async Task GetOnlineFriends()
        {
            var friendstokenids = _dbContext.MyFriends.Where(x => x.User_Token_ID == CollectionToIdToken[Context.ConnectionId])
                .Select(x => x.User_Token_ID).ToList();

            List<UserInfo> Onlinefriends = new List<UserInfo>();

            foreach (var friend in friendstokenids)
            {

                foreach (var client in _onlineClients)
                {
                    if (friend == client.IDToken)
                        Onlinefriends.Add(client);
                }
            }

            Clients.Client(Context.ConnectionId).SendAsync("GetOnlineFriends", Onlinefriends);
        }


        public async Task SendFightInvite(string OpponentTokenID)
        {
            Clients.Client(tokentouserstatus[OpponentTokenID].Connection_ID).SendAsync("FightInvite", CollectionToIdToken[Context.ConnectionId]);

        }

        public async Task YesAnswerToInvite(bool answer, string sendertokenid)
        {
            Clients.Client(tokentouserstatus[sendertokenid].Connection_ID)
                .SendAsync("AcceptForInvite", answer, CollectionToIdToken[Context.ConnectionId]);
        }
        public async Task NoAnswerToInvite(bool answer, string sendertokenid)
        {
            Clients.Client(tokentouserstatus[sendertokenid].Connection_ID).SendAsync("AcceptForInvite", answer);
        }

        public async Task GameFinished()
        {
            tokentouserstatus[CollectionToIdToken[Context.ConnectionId]].Opponent_TokenID = null;

            Clients.Client(Context.ConnectionId).SendAsync("DeleteOpponentInfo");
        }

        private async Task<List<GetQuestionsResponse>> GetQuestions()
        {
            var questions = _dbContext.Questions.AsQueryable().OrderBy(x => Guid.NewGuid()).Take(DefaultQuestionCount).ToList();


            if (questions == null)
            {
                return null;
            }

            return questions.ConvertAll<GetQuestionsResponse>(question => GetQuestionsResponse.CreateClass(question));

           
        }





    }

    public class CLientDTO
    {
        public string TokenID { get; set; }
    }

    public class UserInfo
    {
        public string IDToken { get; set; }

        public string Username { get; set; }

        public int Point { get; set; }

    }

    public class info
    {
        public string Connection_ID { get; set; }

        public string Opponent_TokenID { get; set; }

        public bool WantToplay { get; set; }

        public bool IsConnected { get; set; }

        public DateTime Disconnnected_Time { get; set; }

    }
}

//user send opponent idtoken
//change status for deleting and add datetime