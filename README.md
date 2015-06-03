## Codename: 
DiSConnected

## Overview: 
A disconnected approach to Sitecore using Sitecore API, WebApi, and AngularJS.  This project should only serve as a starterkit to build off of.  Inline documentation was provided to help guide you in extending it.

### Setup? ###

* Should be pretty straightforward, what you will need to do is update the Sitecore Web project publish profile to point at your Sitecore install, and copy the necessary .dlls to the `/lib/Sitecore` directory listed in the README.txt

### What's all in it? ###

* A Sitecore Project intended to be published over the top of an existing Sitecore Install
* A MVC Project that is intended to run on a different instance/server to serve as the head of the site, referencing the Sitecore backend endpoints

### Ok, in a paragraph, how does it work? ###

The Angular Web MVC site serves as the head, using *templates* and *directives* to provide presentation and the *factories* to provide the content (*please refer to `/App/factories/article.js` for example*).  The actual RESTful urls reside in the *web.config* of the front end site, and are passed thru the controller to the front end for the angular *factories* to consume.  These RESTful endpoints are created using WebApi controllers in the Sitecore Web project and map to actual paths using *RouteAttributes* on the controllers themselves (*refer to `/Controllers/ArticlesController.cs` for example*).  Sitecore 7.2 was the version this was initially developed against, but should be able to work with other versions with little (hopefully no) modification needed.

A special thanks to Justin Firth for providing me with the groundwork for the angular project.

Enjoy.