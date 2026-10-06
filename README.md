# 🧪 Laboratorio 4: Base de Datos - CRUD

**Fecha:** 24/09/2026

## 📚 Contenido del Repositorio

En este laboratorio se desarrolló una aplicación de escritorio utilizando **C#** y **Windows Forms**, conectada a una base de datos **MySQL**.
El objetivo principal fue crear un sistema para administrar productos mediante operaciones **CRUD** (Crear, Leer, Actualizar y Eliminar), permitiendo trabajar con información textual, numérica e imágenes.

Durante el laboratorio se trabajó con:
* 🗄️ Conexión a una base de datos MySQL.
* 📦 Registro y consulta de productos.
* 🖥️ Interfaz gráfica utilizando Windows Forms.
* 📊 Visualización de datos mediante un `DataGridView`.
* 🖼️ Carga y almacenamiento de imágenes de productos.
* 🔍 Búsqueda y filtrado de registros.
* 🧩 Uso de clases y objetos para organizar el código.

## 🛠️ Tecnologías Utilizadas

* 💻 **Lenguaje:** C#
* 🧩 **Framework:** .NET
* 🖥️ **IDE:** Visual Studio
* 🪟 **Interfaz gráfica:** Windows Forms
* 🗄️ **Base de datos:** MySQL
* 🛠️ **Herramienta de gestión:** MySQL Workbench
* 📦 **Paquete utilizado:** `MySql.Data`
* 📚 **Control de versiones:** Git & GitHub

## 📸 Capturas de Pantalla y Problemas

### 🗄️ Problema 1: Conexión a la base de datos MySQL

En este ejercicio se estableció la conexión entre la aplicación desarrollada en C# y la base de datos MySQL.
Se creó una clase de conexión utilizando `MySqlConnection`, que permite comunicarse con la base de datos y realizar las operaciones necesarias.
La base de datos utilizada contiene una tabla llamada `productos`, con campos para almacenar el identificador, nombre, precio, cantidad e imagen del producto.
<img width="1222" height="401" alt="result" src="https://github.com/user-attachments/assets/a7f8aa7e-fb90-42bb-8664-f309d2475e92" />


### 📦 Problema 2: Creación de la interfaz de productos

En este ejercicio se diseñó una interfaz gráfica en Windows Forms para ingresar y visualizar información de los productos.
Se utilizaron diferentes controles, entre ellos:
* 📝 `TextBox` para ingresar datos.
* 🖼️ `PictureBox` para mostrar imágenes.
* 📊 `DataGridView` para visualizar los registros.
* 🖱️ `Button` para ejecutar acciones.
* 🖼️ `ImageList` para asociar imágenes a los botones.

La interfaz permite organizar la información de los productos y facilita la interacción con el usuario.
<img width="1015" height="624" alt="image" src="https://github.com/user-attachments/assets/4aa51866-8e90-4daf-9d48-aaeaded7f469" />


### ➕ Problema 3: Registro y consulta de productos

En este ejercicio se implementaron funciones para insertar y consultar productos en la base de datos.
Se utilizó una clase `Producto` para representar la información de cada registro y una colección de objetos para manejar los productos obtenidos desde MySQL.
También se utilizó un `DataGridView` para mostrar los datos almacenados en la tabla `productos`.
<img width="939" height="631" alt="image" src="https://github.com/user-attachments/assets/22baf4ef-dcaa-4260-b1c6-0d033dc9fd3d" />

### 🖼️ Problema 4: Manejo de imágenes

En este ejercicio se trabajó con la carga, conversión y almacenamiento de imágenes de los productos.
Se utilizó el control `OpenFileDialog` para seleccionar imágenes desde la computadora y el control `PictureBox` para mostrarlas en la interfaz.
Además, se utilizó `MemoryStream` para convertir las imágenes en arreglos de bytes (`byte[]`), permitiendo almacenarlas en la base de datos MySQL mediante un campo de tipo `LONGBLOB`.
<img width="950" height="927" alt="image" src="https://github.com/user-attachments/assets/a361ac43-a2a4-4b62-90a4-05a4dc0f6a04" />


### 🔍 Problema 5: Búsqueda y filtrado de productos

En este ejercicio se implementó una función de búsqueda para facilitar la localización de productos registrados.
La aplicación permite filtrar la información mediante un campo de búsqueda y actualizar los datos mostrados en el `DataGridView`.
Para realizar esta función se utilizaron consultas SQL con parámetros y el evento `TextChanged` del campo de búsqueda.
<img width="977" height="640" alt="image" src="https://github.com/user-attachments/assets/40f8597a-4813-43ab-a243-65db4a0f8b74" />

### ✏️ Problema 6: Operaciones CRUD

En este ejercicio se trabajó con las operaciones básicas de un sistema CRUD:
* ➕ **Crear:** Insertar nuevos productos en la base de datos.
* 👀 **Leer:** Consultar y mostrar los productos registrados.
* ✏️ **Actualizar:** Modificar la información de los productos.
* 🧹 **Limpiar:** Limpia los campos de los textboxes.
* 🗑️ **Eliminar:** Eliminar registros de productos.

Estas operaciones permiten administrar la información almacenada en MySQL desde la aplicación de Windows Forms.
<img width="924" height="602" alt="image" src="https://github.com/user-attachments/assets/9de9344e-d938-4090-ad49-c165c4347ef6" />


## 📁 Estructura de Carpetas o Directorios

```
Lab 4/
│
├── Lab 4.slnx
├── Lab 4.csproj
│
├── Conexion.cs
├── Producto.cs
├── Form1.cs
├── Form1.Designer.cs
├── Program.cs
│
├── Properties/
│   └── ...
│
└── README.md
```

## 👨‍💻 Autor y Contexto

* **Nombre:** Julian Mosquera
* **Institución:** Universidad Tecnológica de Panamá (UTP)
* **Facultad:** Ingeniería en Sistemas
* **Carrera:** Ingeniería en Sistemas y Computación
* **Asignatura:** Herramientas de Programación Aplicada III
* **Grupo:** 1IL133
* **Fecha de Realización:** 24/09/2026
