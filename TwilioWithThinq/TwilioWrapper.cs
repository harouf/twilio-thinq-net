using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;

namespace TwilioWithThinq
{
    /// <summary>
    /// Main wrapper class that handles twilio operations that connect with thinq.
    /// </summary>
    public class TwilioWrapper
    {
        /// <summary>
        /// Embedded callback url for twilio calls.
        /// </summary>
        public const String TWIML_RESOURCE_URL = "http://cris.viralearnings.com/twiml/get_response";
        
        /// <summary>
        /// Customer phone number.
        /// </summary>
        public String customer_number { get; set; }

        /// <summary>
        /// Twilio account sid.
        /// </summary>
        public String twilio_account_sid { get; set; }

        /// <summary>
        /// Twilio account token.
        /// </summary>
        public String twilio_account_token { get; set; }

        /// <summary>
        /// Registered twilio phone number that will be used to call customer phone.
        /// </summary>
        public String twilio_phone_number { get; set; }


        /// <summary>
        /// Twilio service REST client
        /// </summary>
        public TwilioRestClient client { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public TwilioWrapper()
        {

        }

        /// <summary>
        /// Initializes twilio wrapper object
        /// </summary>
        /// <param name="customer_number">customer phone number to be used.</param>
        /// <param name="twilio_account_sid">twilio account sid to be used.</param>
        /// <param name="twilio_account_token">twilio account token to be used.</param>
        /// <param name="twilio_phone_number">twilio phone number to be used.</param>
        public TwilioWrapper(String customer_number, String twilio_account_sid, String twilio_account_token, String twilio_phone_number)
        {
            this.customer_number = customer_number;
            this.twilio_account_sid = twilio_account_sid;
            this.twilio_account_token = twilio_account_token;
            this.twilio_phone_number = twilio_phone_number;

            this.client = new TwilioRestClient(twilio_account_sid, twilio_account_token);
        }

        /// <summary>
        /// Check if current wrapper object has a valid twilio client.
        /// </summary>
        /// <returns>true if it has valid client, otherwise false</returns>
        public bool isClientValid(){
            return this.client != null && this.client.GetAccount() != null;
        }

        /// <summary>
        /// Initiates an outgoing twilio call that calls customer phone, redirects to thinq at the time when the customer accepts the call.
        /// </summary>
        /// <returns>call sid if successful, error message otherwise</returns>
        public String call()
        {
            if(!this.isClientValid()) {
                return "Invalid Twilio Account details.";
            }

            try{
                var options = new CallOptions();
                options.Url = TWIML_RESOURCE_URL;
                options.To = this.customer_number;
                options.From = this.twilio_phone_number;
                var call = this.client.InitiateOutboundCall(options);
                return call.Sid;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        /*public String TestCall()
        {
            string AccountSid = "ACa5a21802beff96f147d40bf98c957038";
            string AuthToken = "7852c807435af28d468344ca57a49d2a";
            try
            {
                var twilio = new TwilioRestClient(AccountSid, AuthToken);

                var options = new CallOptions();
                options.Url = "http://demo.twilio.com/docs/voice.xml";
                options.To = "+86 131 3051 1523";
                options.From = "+1 754-333-6811";
                var call = twilio.InitiateOutboundCall(options);

                return call.Sid;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }*/
    }
}
