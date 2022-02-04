# ventas
Aplicacion para el sistema de ventas

# Codigo fuente de los proyectos:
	- ApiVentas: https://github.com/skyleon13/apiventas/tree/Desarrollo/ApiVentas
	- Ventas.exe: https://github.com/skyleon13/ventas/tree/Desarrollo/Ventas
	

# Prerrequisitos necesarios en el equipo
	- Servidor SQL server 2019 para la BD
	- Servidor IIS 10 para montar el api (Puede usarse IIS Express desde visual studio)	
	- .Net Framework 4.5 para correr la Aplicacion
	
# Instrucciones para la base de datos
	- Se debe tomar el script Script_BD.sql que esta en la ruta: https://github.com/skyleon13/apiventas/tree/Desarrollo/Scripts%20BD
	- Se debe ejecutar su contenido en SQL sever management studio (Este creara la BD y las tablas) 
	
# Instrucciones para levantar el API
	1: Usando Visual Studio 2019
		- Abrir el pryecto "apiventas" con visual studio 2019 y ejecutarlo directamente con IIS express
		- Este metodo levantará el servicio en "localhost:44349"
		
	2: Usando servidor IIS
		- Se debe descomprimir la carpeta "apiventas" que esta en: https://github.com/skyleon13/apiventas/tree/Desarrollo/Empaquetado
		- Se debe colocar la carpeta obtenida en C:\inetpub\wwwroot
		- Desde el administrador de IIS se debe elegir la opcion "Sitios" -> "Crear sitio web"
			- Capturamos un nombre "apiventas"
			- Seleccionamos la carpeta que acabamos de poner en wwwroot\apiventas
			- Capturamos un puerto deseado "86" (Puede ser cualquiera)
			- Guardamos y verificamos que el sitio este corriendo.
			
# Instrucciones para ejecutar la aplicacion
	- Descargamos y extraemos el archivo "Ejecutable.zip" que se encuenta en: https://github.com/skyleon13/ventas/tree/Desarrollo
	- Abrimos el archivo Ventas.exe.config y en la etiqueta apiventas ponemos ip:puerto donde este corriendo el api (El que configuramos al levantarlo)
	- Abrimos el archivo Ventas.exe y podemos utilizar las opciones del sistema.
