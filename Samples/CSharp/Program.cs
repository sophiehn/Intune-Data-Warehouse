﻿namespace IntuneDataWarehouseSamples
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using Microsoft.IdentityModel.Clients.ActiveDirectory;

    class Program
    {
        static void Main(string[] args)
        {
            /**
             * TODO: Replace the below values with your own.
             * emailAddress - The email address of the user that you will authenticate as.
             *
             * password     - The password for the above email address.
             *                This is inline only for simplicity in this sample. We do not 
             *                recommend storing passwords in plaintext.
             *
             * applicationId - The application ID of the native app that was created in AAD.
             *                 For more details, refer to these docs: TODO: ** ADD DOC LINK **
             *
             * warehouseUrl   - The data warehouse URL for your tenant. This can be found in 
             *                  the Azure portal. TODO: ** ADD DOC LINK **
             * 
             * collectionName - The name of the warehouse entity collection you would like to 
             *                  access.
             */
            var emailAddress = "intuneadmin@yourcompany.com";
            var password = "password_of(intuneadmin@yourcompany.com)";
            var applicationId = "8d699e29-3b54-4c6a-91cc-e537b4680fed";
            var warehouseUrl = "https://fef.msua01.manage.microsoft.com/ReportingService/DataWarehouseFEService?api-version=beta";
            var collectionName = "dates";

            var adalContext = new AuthenticationContext("https://login.windows.net/common/oauth2/token");
            AuthenticationResult authResult = adalContext.AcquireTokenAsync(
                resource: "https://api.manage.microsoft.com/",
                clientId: applicationId,
                userCredential: new UserPasswordCredential(emailAddress, password)).Result;

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authResult.AccessToken);

            var uriBuilder = new UriBuilder(warehouseUrl);
            uriBuilder.Path += "/" + collectionName;

            HttpResponseMessage response = httpClient.GetAsync(uriBuilder.Uri).Result;

            Console.Write(response.Content.ReadAsStringAsync().Result);
            Console.ReadKey();
        }
    }
}
