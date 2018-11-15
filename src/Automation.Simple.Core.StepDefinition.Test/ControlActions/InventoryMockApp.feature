Feature: Mock Inventario
	Como automatizador de escenarios de prueba
	Necesito escribir pruebas de integracion
	Con el objetivo de evitar errores

Background: Set Up scenario
Given I go to 'http://mock1.com'

@BVT
Scenario: Login a aplicacion mock 
Given I fill 'admin' in Usuario field
And I fill 'admin' in Password field
When I click Acceder button
Then I should see 'Administrador' in Informacion de usuario field on cabecera
When I click Productos on menu
	And I click Agregar Producto on pagina
	And I fill 'PROD122' in Codigo de barras
	And I fill 'PROD ASD' in Nombre
	And I fill 'Description ' in Descripcion
	And I fill '90.1' in Precio de entrada
	And I fill '80.1' in precio de salida
	And I fill 'Lts' in Unidad
	And I fill 'Botella' in presentacion
	And I fill '10' in Minima en inventario
	And I fill '90' in Inventario inicial
	And I click Agregar producto button
#Then I should see the following values in Lista de Productos
#	| Campo  | Valor |
#	| Codigo | 331   |
#	| Nombre | 331   |
