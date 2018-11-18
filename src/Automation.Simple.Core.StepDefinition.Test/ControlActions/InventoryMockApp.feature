Característica: Mock Inventario
	Como automatizador de escenarios de prueba
	Necesito escribir pruebas de integracion
	Con el objetivo de evitar errores

Antecedentes: Set Up scenario
Dado I go to 'http://mock1.com'

@BVT
Escenario: Login a aplicacion mock 
Dado Se escribe 'admin' en campo Usuario
Y Se escribe 'admin' en campo Password
Cuando Se hace click en boton Acceder
Entonces Deberia ver 'Administrador' en Informacion de usuario field en cabecera
Cuando Se hace click en Productos en menu
	Y Se hace click en Agregar Producto en pagina
	Y Se escribe 'PROD122' en Codigo de barras
	Y Se escribe 'PROD ASD' en Nombre
	Y Se escribe 'Description ' en Descripcion
	Y Se escribe '90.1' en Precio de entrada
	Y Se escribe '80.1' en precio de salida
	Y Se escribe 'Lts' en Unidad
	Y Se escribe 'Botella' en presentacion
	Y Se escribe '10' en Minima en inventario
	Y Se escribe '90' en Inventario inicial
	Y Se hace click en Agregar producto
#Entonces I should see the following values in Lista de Productos
#	| Campo  | Valor |
#	| Codigo | 331   |
#	| Nombre | 331   |
