# Ejemplos de eventos usando expresiones lambda
Código de ejemplo del artículo: Crear métodos de evento con expresiones lambda para asignarlos manualmente (VB y C#)
<br>
<br>
El artículo está publicado en elguillemola.com: <a href="http://www.elguillemola.com/2020/09/crear-metodos-de-evento-con-expresiones-lambda-para-asignarlos-manualmente-vb-y-c/">Crear métodos de evento con expresiones lambda para asignarlos manualmente (VB y C#)</a><br>
<br>
Si tienes algún comentario, mejor que lo dejes en el post original, ya que esto es solo para tener el código fuente usado en dicho artículo.<br>
<br>
<br>
Aquí te dejo los conceptos explicados (copiados directamente del post)<br>
<br>
## Lo explicado en el artículo
Sin el código.<br>
<br>
### Crear métodos de evento con expresiones lambda para asignarlos manualmente (VB y C#)<br>
<br>
Pues eso… hoy te voy a explicar cómo crear métodos lambda (o métodos de evento que usan expresiones lambda) para asignar a los eventos de los controles. Esto lo estoy usando últimamente hasta que la gente de Visual Studio mejore el diseñador de formularios para .NET 5.0 (y .NET Core 3.1) ya que… deja mucho que desear y da muchos quebraderos de cabeza… nada que ver con el diseñador de WinForm (Windows.Forms Designer) de .NET Framework.<br>
<br>
Pero aparte de que el editor tenga sus cosas que hay que arreglar… y la verdad no sé si lo arreglarán, ya que ni con el diseñador de formularios de C# va bien. Y no ya porque esté usando el Visual Studio 2019 Preview (actualmente tengo instalada la versión 16.8.0 Preview 3.1, que es la última que hay a día de hoy 28 de septiembre de 2020), si no porque tampoco va en la versión normal de Visual Studio y con el .NET Core soportado, que es la versión 3.1.<br>
<br>
Antes de seguir con el código de ejemplo, te explico lo que dice la documentación de Microsoft (Microsoft docs) sobre las expresiones lambda en Visual Basic.<br>
<br>
Y esta es la definición de las expresiones lambda en C#.<br>
<br>
### ¿Qué son las expresiones lambda?<br>
Definición en la documentación de Visual Basic:<br>
<br>
Una expresión lambda es una función o subrutina sin un nombre que se puede usar siempre que un delegado sea válido. Las expresiones lambda pueden ser funciones o subrutinas y pueden ser de una o varias líneas. Puede pasar valores del ámbito actual a una expresión lambda.<br>
<br>
Lambda (expresiones) (Visual Basic) en la Documentación de Microsoft.<br>
Definición en la documentación de C#:<br>
<br>
Una expresión lambda es una expresión que tiene cualquiera de estas dos formas:<br>
Una lambda de expresión que tiene una expresión como cuerpo:<br>
(input-parameters) => expression<br>
<br>
Una lambda de instrucción que tiene un bloque de instrucciones como cuerpo:<br>
(input-parameters) => { <sequence-of-statements> }<br>
<br>
Use el operador de declaración lambda => para separar la lista de parámetros de la lamba de su cuerpo. Para crear una expresión lambda, especifique los parámetros de entrada (si existen) a la izquierda del operador lambda y una expresión o bloque de instrucciones en el otro lado.<br>
<br>
Expresiones lambda (referencia de C#) en la documentación de Microsoft.<br>
Lo que dice la documentación sobre el operador =><br>
El token => se admite de dos formas: como el operador lambda y como un separador de un nombre de miembro y la implementación del miembro en una definición de cuerpo de expresión.<br>
<br>
  <h4>Ejemplos de expresiones lambda</h4>
Imagina que quieres asignar al evento Click de un botón llamado buttonAbrir y cuando se produzca quieres indicarle que llame al método Abrir, en lugar de crear un método específico, que de forma predeterminada tendrá el siguiente aspecto:<br>
<br>
Puedes hacerlo de esta otra forma (por ejemplo dentro del evento Load del formulario):<br>
<br>
En este último de ejemplo, se crear el método de evento usando una expresión lambda, que como ves solamente usa Sub(), sin argumentos. Esto es así porque Visual Basic lo permite. Si eso mismo se hiciera con C# habría que indicar expresamente los parámetros de esa expresión lambda, aquí te muestro el mismo código (o casi) pero para C#.<br>
<br>
El casi, es porque en algún sitio del código de C# hay que asignar el método Form1_Load al evento Load del formulario (que sería el equivalente al Handles Me.Load del código de Visual Basic):<br>
<br>
Todo este código mostrado sería la forma más simple de asignar una expresión lambda para indicar el método que manejará el evento.<br>
<br>
Utilizar el mismo método de evento (o expresión lambda) en varios controles<br>
Pero imagina que quieres hacer la misma asignación de una expresión lambda en varios controles.<br>
<br>
Si los controles están declarados con WithEvents y estás usando el diseñador de Windows Forms de Visual Studio para una aplicación de .NET Framework, podrías hacer algo como esto para que el método variosUndo_Click que para manejar los eventos de un botón llamado buttonUndo y para un menú con nombre menuUndo estaría definido de esta forma:<br>
<br>
<br>
  <h5>Nota:</h5>
En C# (que yo sepa) no hay equivalente para esto, habría que asignar de forma independiente cada manejador de evento.<br>
<br>
Sigamos con el código para usar una expresión lambda para hacer lo mismo que en el ejemplo anterior.<br>
<br>
Lo primero es definir la expresión lambda:<br>
<br>
<h5>Nota:</h5>
He usado los guiones bajos para que el código sea más legible.<br>
La he declarado explícitamente como del tipo EventHandler porque están declaradas en el cuerpo del la clase. Si estuviese dentro de un método puedes declararlas con:<br>
<br>
Y la asignación a los eventos de esos dos controles antes mencionados (buttonUndo y menuUndo) la haremos de esta forma:<br>
<br>
Y ya que estamos, y para terminar, te muestro el equivalente al código anterior, pero sin haber declarado la variable (con la expresión lambda) lambdaUndo:<br>
<br>
C# no permite definir expresiones lambda a nivel de clase si accede a miembros no estáticos<br>
En C# no podemos definir la expresión lambda a nivel de clase si accede a un miembro no estático de dicha clase. En VB si se puede (como has podido comprobar).<br>
Así que, si queremos definirlo a nivel de clase, habrá que tener en cuenta que si accede a algún control u otro objeto declarado en esa clase, deben estar definidos con la cláusula static.<br>
<br>
Aquí tienes la forma de hacer lo mismo que en el código de Visual Basic (he añadido la definición de richTextBoxCodigo como estático (compartido, Shared en Visual Basic):<br>
<br>
<h5>Nota:</h5>
Al definir los parámetros de la expresión lambda en C# no es necesario indicar el tipo de datos de cada argumento, con indicar los nombres de los identificadores (variables) es suficiente, tal como te moestraré en otro ejemplo.<br>
<br>
Una cosa interesante, y a tener en cuenta, en la definición de la expresión lambda asignada a la variable lambdaUndo es que se usa una expresión if como resultado. En ese caso es obligatorio usar las llaves de apertura y cierre: { };, si no, dará error.<br>
Cuando se usa una llamada a un método no es necesario ponerlo entre las llaves.<br>
<br>
Lo de interesante en el párrafo anterior es porque en los ejemplos que vi en internet, para saber porqué no podía declarar (sin errores) esa expresión lambda, no encontré ninguno que usara una expresión al estilo del if, todos los ejemplos eran usando llamadas a métodos o como si fuesen la definición de una llamada a un método. Y además el único ejemplo que me topé definiendo la expresión lambda en el cuerpo de la clase, llamaba a MessageBox y nada que me aclarara las dudas.<br>
<br>
Sí, puedes estar pensando que si hubiese hecho caso al mensaje ese de que un inicializador de un campo no puede referenciar a un método no estático, etc. (A field initializer cannot reference the non-static field, method or property ‘Form1.richTextBoxCodigo’) me lo decía todo, pero es que antes de que me mostrase ese mensaje me decía un montón de cosas que no tenían nada que ver con eso.<br>
<br>
Aquí tienes todos los errores que mostraba al asignar ese método anónimo sin usar las llavecitas de las narices…<br>
<br>
Lista de los errores mostrados al no usar las llaves alrededor de la expresión if.<br>
Y todos en la misma línea (la 39) que es la que define lambdaUno de esta forma (que ya sabemos que es errónea):<br>
private EventHandler lambdaUndo = (object sender, EventArgs e) => if(richTextBoxCodigo.CanUndo) richTextBoxCodigo.Undo();<br>
<br>
Error CS1525  Invalid expression term 'if'<br>
Error CS1003  Syntax error, ',' expected<br>
Error CS1002  ; expected<br>
Error CS8124  Tuple must contain at least two elements.<br>
Error CS0246  The type or namespace name 'richTextBoxCodigo' could not be found (are you missing a using directive or an assembly reference?)<br>
Error CS0246  The type or namespace name 'richTextBoxCodigo' could not be found (are you missing a using directive or an assembly reference?)<br>
Error CS0538  'richTextBoxCodigo' in explicit interface declaration is not an interface<br>
Error CS0501  'Form1.Undo()' must declare a body because it is not marked abstract, extern, or partial<br>
<br>
<h5>Nota:</h5>
Ahora que he copiado la definición de las expresiones lambda en la documentación de C# (para pegarla arriba), al ver lo que en esa documentación llaman lambda de instrucción, veo que ahí se indica que hay que encerrar la expresión entre llaves.<br>
<br>
En mi defensa te dirá que la documentación de C# la he leído cuando estaba revisando lo que ya había escrito hasta después del siguiente ejemplo. En fin…
Aunque lo mismo tampoco me hubiese enterado…<br>
¡Qué torpe es el Guille!<br>
<br>
La otra solución es definir esa expresión lambda dentro del método que sea, por ejemplo Form1_Load.<br>
En este caso no tenemos que definir el control richTextBoxCodigo como static y la expresión lambda la definimos dentro del evento Load del formulario.
Esto está bien si no necesitamos usar esa expresión lambda imbuida en una variable en algún otro método de esa clase.<br>
<br>
Este sería el código de ejemplo:<br>
<br>
Pero en mi caso en particular esta última solución no me sirve, ya que esa expresión lambda la tenía que usar en varios métodos de la misma clase.<br>
<br>
<h5>Nota (repetimos):</h5>
Al definir los parámetros de la expresión lambda en C# no es necesario indicar el tipo de datos de cada argumento, con indicar los nombres de los identificadores (variables) es suficiente, tal como te muestre en el código anterior.<br>
<br>
Y hasta aquí hemos llegado… (el fin)<br>
<br>
<br>
Espero que te sea de utilidad.<br>
Guillermo<br>
<br>
Actualizado el 28 de septiembre de 2020 a las 09:05

