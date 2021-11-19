<p align="center"> 
  <img src="https://cdn-icons-png.flaticon.com/512/32/32840.png" alt="Логотип" width="80px" height="80px">
</p>
<h1 align="center"> Skorpion </h1>
<p>Интернет-магазин компьютерных инди-игр</p>
<h2>О приложении</h2>
<p>Skorpion - это простой способ находить, скачивать и распространять инди-игры онлайн. Являетесь ли вы разработчиком, который хочет загрузить свою игру, или просто кем-то, кто ищет что-то новое, чтобы поиграть.</p>
<p>Skorpion предоставляет следующие возможности:</p>
<ul>
  <li><b>Продавать/Покупать игры</b></li>
  <li><b>Система профилей и друзей</b></li>
  <li><b>Рекомендательная система</b></li>
</ul>
<h2>Технологии</h2>
<ul>
  <li><b>ASP.NET Core</b></li>
    <ul>
      <li><b>ASP.NET Core MVC</b></li>
      <li><b>ASP.NET Core Identity</b></li>
      <li><b>Razor Pages</b></li>
    </ul>
  <li><b>Entity Framework Core</b></li>
  <li><b>Boostrap</b></li>
  <li><b>PostgreSQL</b></li>
</ul>
<h2>Начало работы</h2>
<p>Нужно скачать следующие вещи:</p>
<ul>
  <li>.NET 6</li>
  <li>PostgreSQL</li>
</ul>
<p>В файле Startup.cs найти код:</p><code>services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(Configuration.GetConnectionString("IskanderConnection")));</code>  
<p></p>
<p>В параметры GetConnectionString() отправить свою строку подключения. Её можно найти в файле appsettings.json, там же эту строку можно отредактировать под себя</p>
<p>Дальше необходимо подготовить базу данных. Открываем консоль в папке проекта, прописываем следующие команды</p>
<ul>
  <li><code>dotnet ef migrations add InitialCreate</code></li>
  <li><code>dotnet ef database update</code></li>
</ul>
<p>Теперь нужно заполнить базу данных. Для этого запускаем data.sql скрипт</p>
<p>Готово! Запускаем проект с помощью IDE или с помощью командной строки, введя <code>dotnet watch</code></p>
