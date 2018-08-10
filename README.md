# Sklep muzyczny "Fajna Nuta"
Siemka
repozytorium na które właśnie patrzysz zostało napisane w jezyku C# z wykorzystaniem framework'u ASP.NET MVC 5. Przedstawia aplikację
internetową "Fajna Nuta" pozwalająca na zakup piosenek (aplikacja nie jest ukończona). Przy tworzeniu sklepu muzycznego 
strałem się użyć najnowszych wzorców i technolgi takich jak: testy jednostkowe,mechanizm migracji kontener DI Ninjec (patrzy dół)
i wiele innych


```c#
 /// <summary>
 /// Setting bindings with using Ninject
 /// </summary>
private void AddBindings()
{
   kernel.Bind<ISongRepository>().To<DefaultSongRepository>();
   kernel.Bind<ISessionManager>().To<DefaultSessionManager>();
}
```
## Wyglad
![alt text](https://github.com/NoEducation/Sklep-Muzyczny-1.0-/blob/master/SklepMuzyczny/SklepMuzyczny/Content/img/mainIndexExample.png)
#### [Fajna nuta - link]()

## Licencja

This project is released under the MIT Licence. See the bundled LICENSE file for details.

## Autor

Dominik Atrasik
