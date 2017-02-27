using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Twilio;
using Twilio.TwiML;
using Twilio.TwiML.Mvc;

namespace TwilioClientServer.Controllers
{
    public class ClientController : Controller
    {
		private const string _clientName = "xamarin";//alice
		private const string AccoutnSID = "AC16fb70203b22a1a522498efb22fc34c6";
		private const string AuthToken = "9e5c32ae4ab8762c54392b737add26ac";
		private const string TwiMLAppSID = "APd6ee2f314229d9980f3666accc0f7d3b";

        // GET: Client/Token?ClientName=alice
        public ActionResult Token(string clientName = _clientName)
        {
            // Create a TwilioCapability object passing in your credentials.
            var capability = new TwilioCapability(AccoutnSID, AuthToken);

            // Specify that this token allows receiving of incoming calls
            capability.AllowClientIncoming(clientName);

            // Replace "AP*****" with your TwiML App Sid
            capability.AllowClientOutgoing(TwiMLAppSID);

            // Return the token as text
            return Content(capability.GenerateToken());
        }

        // /Client/CallXamarin
        public ActionResult CallXamarin()
        {
            var response = new TwilioResponse();
            response.Dial(new Client(_clientName));
            return new TwiMLResult(response);
        }

        // /Client/InitiateCall?source=5551231234&target=5554561212
        public ActionResult InitiateCall(string from, string to)//Source and Target do not work for incomming calls from a phone, must be From and To
        {
            var response = new TwilioResponse();

            // Add a <Dial> tag that specifies the callerId attribute
            response.Dial(to, new { callerId = from });

            return new TwiMLResult(response);
        }

        public ActionResult Test()
        {
            var response = new TwilioResponse();

            // Add a <Dial> tag that specifies the callerId attribute
            response.Say("Test message for VoIP project.");

            return new TwiMLResult(response);
        }

        public ActionResult Test2(string from, string to)
        {
            var response = new TwilioResponse();

            response.Say("From: " + from + ", To: " + to);

            return new TwiMLResult(response);
        }
    }
}