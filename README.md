# Music  store  „Fajna Nuta” ![.NET Core](https://github.com/NoEducation/Sklep-Muzyczny-1.0-/workflows/.NET%20Core/badge.svg?branch=master)
Hello, the repository you are just looking right now  has been written in C # using the ASP.NET MVC 5  framework. Repository show  internet applications “ Fajna Nuta” which provide opportunity to buy songs (website isn’t finished ). When I was developed  music store I try to use design patterns, unity tests, migration mechanism, dependency injection (look down)  and many more


```c#
 /// <summary>
 /// Example of dependency resolver Ninject used in my Project
 /// </summary>
private void AddBindings()
{
   kernel.Bind<ISongRepository>().To<DefaultSongRepository>();
   kernel.Bind<ISessionManager>().To<DefaultSessionManager>();
}
```
## Appearance
![alt text](https://github.com/NoEducation/Sklep-Muzyczny-1.0-/blob/master/SklepMuzyczny/SklepMuzyczny/Content/img/mainIndexExample.png)
## [Fajna nuta (please be patient)](https://sklepmuzyczny.azurewebsites.net)
 There is some issues with appearance of published website which I try to resolve.
 

## Author

Dominik Atrasik
