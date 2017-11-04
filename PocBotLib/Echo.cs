﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildABot.Core;
using BuildABot.Util;
using System.ComponentModel.Composition;
using BuildABot.Core.MessageHandlers;
using Log;
using System.Web;
using System.Net;
using System.IO;
using RequestApi;
using System.Configuration;

namespace PocBotLib
{
    [Export(typeof(MessageHandler))]
    public class Echo : SingleStateMessageHandler
    {
        public Echo()
        :base(".*"){ 
        }
        protected override string GetInitialHandlingText()
        {
            Logger.Debug("GetInitialHandlingText function");
            return base.GetInitialHandlingText();
        }

      
        public override Reply Handle(Message message) {
            Logger.Debug("Handle function");
            Reply reply = new Reply();
            try
            {
                if (message.Content == "request")
                {
                    //request_api
                    RequestApi.RequestApi request = new RequestApi.RequestApi();
                    Logger.Debug("request - Handle " + request.ToString());
                    String endpointBot =  ConfigurationManager.AppSettings["endpoint"];
                    Logger.Debug("endpoint string " + endpointBot);
                    String response  = request.getResponse(endpointBot);
                    Logger.Debug("API response : " + response);

                    reply.Add("api : " + response);
                }
                else {
                    reply.Add("user said : " + message.Content);
                    Logger.Debug("bot message = " + message.Content);
                }

            }
            catch (Exception e)
            {
                reply.Add("error bot : " + e.Message);
                Logger.Debug("error :" + e);
            }
            return reply; 
        }
       
    }
}
