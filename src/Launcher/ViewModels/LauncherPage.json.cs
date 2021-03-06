using Starcounter;                                  // Most stuff relating to the database, JSON and communication is in this namespace
using Starcounter.Templates;
using Starcounter.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Reflection;
using System.Web;
using Starcounter.Advanced.XSON;
using Concepts.Ring8.Tunity;
using Colab.Common;

namespace Launcher
{

    [LauncherPage_json]                                       // This attribute tells Starcounter that the class corresponds to an object in the JSON-by-example file.
    partial class LauncherPage : Page
    {
        public Boolean CheckLogin()
        {
            var userSession = Db.SQL<UserSession>("SELECT o FROM Concepts.Ring8.Tunity.UserSession o WHERE o.SessionIdString=?", Session.Current.SessionId).First;
            if (userSession != null)
            {
                return userSession.User != null;
            };
            return false;
        }


        public void SetTitle(String subtitle)
        {
            Title = ColabConfiguration.Get<String>(ColabConfig.TITLE) + " "  + subtitle;
        }

        // public Boolean signedIn => CheckLogin();

        public Boolean SignedInCB
       {
            get
            {
                return CheckLogin();
            }       
        }

        [LauncherPage_json.searchBar]
        partial class SearchBar : Json
        {


            void Handle(Input.query action)
            {
                if (String.IsNullOrEmpty(action.Value))
                {
                    this.previewVisible = false;
                    this.previewResult = null;
                    return;
                }
                string uri = UriMapping.MappingUriPrefix + "/search?query=" + HttpUtility.UrlEncode(action.Value);
                this.previewVisible = true;
                this.previewResult = Self.GET<Json>(uri, () =>
                {
                    var p = new Page();
                    return p;
                });

            }
            /*
            void Handle(Input.submit action)
            {
                string uri = "/launcher/search?query=" + this.query;
                Response resp = Self.GET(uri);

                searchEngineResultPageUrl = uri;
            }*/

            void Handle(Input.close action)
            {
                //this.previewResult = null;
                this.previewVisible = false;
            }

            public String OptionsCB
            {
                get
                {
                    return "";
                }
                set
                {
                }
            }
        }
    }




}