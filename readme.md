# Searchfight 
To determine the popularity of programming languages on the internet we want to you to write an application that queries search engines and compares how many results they return, for example: 
    C:\> searchfight.exe .net java 
    .net: Google: 4450000000 MSN Search: 12354420 
    java: Google: 966000000 MSN Search: 94381485 
    Google winner: .net 
    MSN Search winner: java 
    Total winner: .net 
•   The application should be able to receive a variable amount of words 
•   The application should support quotation marks to allow searching for terms with spaces (e.g. searchfight.exe “java script”) 
•   The application should support at least two search engines 


# Publish

There is a compiled demo at the publish section.
Win-64
.net 5

# Dependencies
It requires appsettings.json to work


EnabledSearchProviders: Defines wich search engines of the available are active (if there are some other like yahoo)
GoogleSearchEngine: Contains the information for create the google json api request.
BingSearchEngine: Contains the information for use the bing 7.0 api request.

Disclaimer: This values are free tier and support a very limited amount of request per second and day

Example:
{
  "EnabledSearchProviders": [ "Google", "Bing" ],
  "GoogleSearchEngine": {
    "Provider": "Google",
    "CustomEngine": "017576662512468239146:omuauf_lfve",
    "Key": "AIzaSyDHnR36PgiLNYcLfkP2dkmb_yn24gPSaGE",
    "URI": "https://www.googleapis.com/customsearch/v1"
  },
  "BingSearchEngine": {
    "Provider": "Bing",
    "Key": "2829e791bcf74f7597c75a741a30877d",
    "URI": "https://api.bing.microsoft.com/v7.0/search"
  }
}
