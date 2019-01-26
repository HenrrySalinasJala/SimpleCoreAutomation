Característica: Mock Inventario
	Como automatizador de escenarios de prueba
	Necesito escribir pruebas de integracion
	Con el objetivo de evitar errores inesperados en el sistema

@BVT
Escenario: Test Login
Dado Se navega a 'http://mock1.com'
	Cuando Se autentica como usuario por defecto
Entonces Debería ver 'Administrador' en Informacion de usuario en cabecera

@BVT
Escenario: Test Crear categoría
Dado Se navega a 'http://mock1.com'
Cuando Se autentica como usuario por defecto
Entonces Debería ver 'Administrador' en Informacion de usuario en cabecera
Cuando Se hace click en Catalogos en menu
	Y Se hace click en Categorias en menu
	Y Se hace click en Nueva categoria en pagina
Entonces Debería ver botón Agregar categoria mostrado en pagina
Cuando Se escribe '[random+3]-[random+5]' en Nombre
	Y Se hace click en Agregar categoria
Entonces Debería ver botón Nueva categoria mostrado en pagina
	Entonces Deberia ver los siguientes valores en Lista de categorias en pagina
	| Columna | Valor                  |
	| Nombre  | Tornillos;Herramientas |
	Y No Deberia ver los siguientes valores en Lista de categorias en pagina
	| Columna | Valor     |
	| Nombre  | NO-EXISTE |

@BVT
Escenario: Test Crear producto
Dado Se navega a 'http://mock1.com'
Cuando Se autentica como usuario por defecto
Entonces Debería ver 'Administrador' en Informacion de usuario en cabecera
Cuando Se hace click en Productos en menu
	Y Se hace click en Agregar Producto en pagina
	Y Se escribe '[random+8]' en Codigo de barras
	Y Se escribe '[random+5]' en Nombre
	Y Se selecciona 'Herramientas' en combo-box Categoria 
	Y Se escribe 'Description [random+17]' en Descripcion
	Y Se escribe '90.1' en Precio de entrada
	Y Se escribe '80.1' en precio de salida
	Y Se escribe 'Lts' en Unidad
	Y Se escribe 'Botella' en presentacion
	Y Se escribe '10' en Minima en inventario
	Y Se escribe '90' en Inventario inicial
	Y Se hace click en Agregar producto

@BVT
Escenario: Test Date Field
Dado Se navega a 'http://mock1.com'
	Cuando Se autentica como usuario por defecto
Entonces Debería ver 'Administrador' en Informacion de usuario en cabecera
Cuando Se hace click en Reportes en menu
	Y Se hace click en Reporte de Ventas en menu
	Y Se selecciona 'newCliente' en combo-box Tipo de reporte
	Y Se escribe '[today]' en Desde
	Y Se escribe '[today]' en Hasta
	Y Se hace click en Procesar
Entonces Deberia ver '[today]' en Desde