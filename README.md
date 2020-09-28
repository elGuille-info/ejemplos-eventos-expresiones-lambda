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
<br>
Crear métodos de evento con expresiones lambda para asignarlos manualmente (VB y C#)
Pues eso… hoy te voy a explicar cómo crear métodos lambda (o métodos de evento que usan expresiones lambda) para asignar a los eventos de los controles. Esto lo estoy usando últimamente hasta que la gente de Visual Studio mejore el diseñador de formularios para .NET 5.0 (y .NET Core 3.1) ya que… deja mucho que desear y da muchos quebraderos de cabeza… nada que ver con el diseñador de WinForm (Windows.Forms Designer) de .NET Framework.

Pero aparte de que el editor tenga sus cosas que hay que arreglar… y la verdad no sé si lo arreglarán, ya que ni con el diseñador de formularios de C# va bien. Y no ya porque esté usando el Visual Studio 2019 Preview (actualmente tengo instalada la versión 16.8.0 Preview 3.1, que es la última que hay a día de hoy 28 de septiembre de 2020), si no porque tampoco va en la versión normal de Visual Studio y con el .NET Core soportado, que es la versión 3.1.

Antes de seguir con el código de ejemplo, te explico lo que dice la documentación de Microsoft (Microsoft docs) sobre las expresiones lambda en Visual Basic.

Y esta es la definición de las expresiones lambda en C#.

¿Qué son las expresiones lambda?
Definición en la documentación de Visual Basic:

Una expresión lambda es una función o subrutina sin un nombre que se puede usar siempre que un delegado sea válido. Las expresiones lambda pueden ser funciones o subrutinas y pueden ser de una o varias líneas. Puede pasar valores del ámbito actual a una expresión lambda.

Lambda (expresiones) (Visual Basic) en la Documentación de Microsoft.
Definición en la documentación de C#:

Una expresión lambda es una expresión que tiene cualquiera de estas dos formas:
Una lambda de expresión que tiene una expresión como cuerpo:
(input-parameters) => expression

Una lambda de instrucción que tiene un bloque de instrucciones como cuerpo:
(input-parameters) => { <sequence-of-statements> }

Use el operador de declaración lambda => para separar la lista de parámetros de la lamba de su cuerpo. Para crear una expresión lambda, especifique los parámetros de entrada (si existen) a la izquierda del operador lambda y una expresión o bloque de instrucciones en el otro lado.

Expresiones lambda (referencia de C#) en la documentación de Microsoft.
Lo que dice la documentación sobre el operador =>
El token => se admite de dos formas: como el operador lambda y como un separador de un nombre de miembro y la implementación del miembro en una definición de cuerpo de expresión.

Ejemplos de expresiones lambda
Imagina que quieres asignar al evento Click de un botón llamado buttonAbrir y cuando se produzca quieres indicarle que llame al método Abrir, en lugar de crear un método específico, que de forma predeterminada tendrá el siguiente aspecto:

Private Sub buttonAbrir_Click(sender As Object, e As EventArgs) Handles buttonAbrir.Click
    Abrir()
End Sub
Puedes hacerlo de esta otra forma (por ejemplo dentro del evento Load del formulario):

Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load

    AddHandler buttonAbrir.Click, Sub() Abrir()

End Sub
En este último de ejemplo, se crear el método de evento usando una expresión lambda, que como ves solamente usa Sub(), sin argumentos. Esto es así porque Visual Basic lo permite. Si eso mismo se hiciera con C# habría que indicar expresamente los parámetros de esa expresión lambda, aquí te muestro el mismo código (o casi) pero para C#.

private void Form1_Load(object sender, EventArgs e)
{
    buttonAbrir.Click += (object o, EventArgs e) => Abrir();
}
El casi, es porque en algún sitio del código de C# hay que asignar el método Form1_Load al evento Load del formulario (que sería el equivalente al Handles Me.Load del código de Visual Basic):

this.Load += Form1_Load;
Todo este código mostrado sería la forma más simple de asignar una expresión lambda para indicar el método que manejará el evento.

Utilizar el mismo método de evento (o expresión lambda) en varios controles
Pero imagina que quieres hacer la misma asignación de una expresión lambda en varios controles.

Si los controles están declarados con WithEvents y estás usando el diseñador de Windows Forms de Visual Studio para una aplicación de .NET Framework, podrías hacer algo como esto para que el método variosUndo_Click que para manejar los eventos de un botón llamado buttonUndo y para un menú con nombre menuUndo estaría definido de esta forma:

Private Sub variosUndo_Click(sender As Object, e As EventArgs) _

                    Handles buttonUndo.Click, menuUndo.Click

    If richTextBoxCodigo.CanUndo Then richTextBoxCodigo.Undo()
End Sub
Nota:
En C# (que yo sepa) no hay equivalente para esto, habría que asignar de forma independiente cada manejador de evento.

Sigamos con el código para usar una expresión lambda para hacer lo mismo que en el ejemplo anterior.

Lo primero es definir la expresión lambda:

Private lambdaUndo As EventHandler =  _
           Sub(sender As Object, e As EventArgs) _
               If richTextBoxCodigo.CanUndo Then richTextBoxCodigo.Undo()
Nota:
He usado los guiones bajos para que el código sea más legible.
La he declarado explícitamente como del tipo EventHandler porque están declaradas en el cuerpo del la clase. Si estuviese dentro de un método puedes declararlas con:
Dim lambdaUndo = Sub…

Y la asignación a los eventos de esos dos controles antes mencionados (buttonUndo y menuUndo) la haremos de esta forma:

AddHandler buttonUndo.Click, lambdaUndo
AddHandler menuUndo.Click, lambdaUndo
Y ya que estamos, y para terminar, te muestro el equivalente al código anterior, pero sin haber declarado la variable (con la expresión lambda) lambdaUndo:

' Usando expresión lambda no definida previamente
AddHandler buttonUndo.Click, _
           Sub() If richTextBoxCodigo.CanUndo Then richTextBoxCodigo.Undo()
AddHandler menuUndo.Click, _
           Sub() If richTextBoxCodigo.CanUndo Then richTextBoxCodigo.Undo()
C# no permite definir expresiones lambda a nivel de clase si accede a miembros no estáticos
En C# no podemos definir la expresión lambda a nivel de clase si accede a un miembro no estático de dicha clase. En VB si se puede (como has podido comprobar).
Así que, si queremos definirlo a nivel de clase, habrá que tener en cuenta que si accede a algún control u otro objeto declarado en esa clase, deben estar definidos con la cláusula static.

Aquí tienes la forma de hacer lo mismo que en el código de Visual Basic (he añadido la definición de richTextBoxCodigo como estático (compartido, Shared en Visual Basic):

// En C# no permite declararlo fuera del cuerpo de un método
// si no, da error de que el control no está definido (debe ser static)
// A field initializer cannot reference the non-static field, method or property 'Form1.richTextBoxCodigo'
// por tanto: definiendo el richtextbox como static ya funciona

private static RichTextBox richTextBoxCodigo;

private EventHandler lambdaUndo = 
            (object sender, EventArgs e) => 
            {
                  if(richTextBoxCodigo.CanUndo) richTextBoxCodigo.Undo();
            };


    // Usando expresión lambda definida previamente
    buttonUndo.Click += lambdaUndo;
    menuUndo.Click += lambdaUndo;

Nota:
Al definir los parámetros de la expresión lambda en C# no es necesario indicar el tipo de datos de cada argumento, con indicar los nombres de los identificadores (variables) es suficiente, tal como te moestraré en otro ejemplo.

Una cosa interesante, y a tener en cuenta, en la definición de la expresión lambda asignada a la variable lambdaUndo es que se usa una expresión if como resultado. En ese caso es obligatorio usar las llaves de apertura y cierre: { };, si no, dará error.
Cuando se usa una llamada a un método no es necesario ponerlo entre las llaves.

Lo de interesante en el párrafo anterior es porque en los ejemplos que vi en internet, para saber porqué no podía declarar (sin errores) esa expresión lambda, no encontré ninguno que usara una expresión al estilo del if, todos los ejemplos eran usando llamadas a métodos o como si fuesen la definición de una llamada a un método. Y además el único ejemplo que me topé definiendo la expresión lambda en el cuerpo de la clase, llamaba a MessageBox y nada que me aclarara las dudas.

Sí, puedes estar pensando que si hubiese hecho caso al mensaje ese de que un inicializador de un campo no puede referenciar a un método no estático, etc. (A field initializer cannot reference the non-static field, method or property ‘Form1.richTextBoxCodigo’) me lo decía todo, pero es que antes de que me mostrase ese mensaje me decía un montón de cosas que no tenían nada que ver con eso.

Aquí tienes todos los errores que mostraba al asignar ese método anónimo sin usar las llavecitas de las narices…

Lista de los errores mostrados al no usar las llaves alrededor de la expresión if.
Y todos en la misma línea (la 39) que es la que define lambdaUno de esta forma (que ya sabemos que es errónea):
private EventHandler lambdaUndo = (object sender, EventArgs e) => if(richTextBoxCodigo.CanUndo) richTextBoxCodigo.Undo();

Error CS1525  Invalid expression term 'if'
Error CS1003  Syntax error, ',' expected
Error CS1002  ; expected
Error CS8124  Tuple must contain at least two elements.
Error CS0246  The type or namespace name 'richTextBoxCodigo' could not be found (are you missing a using directive or an assembly reference?)
Error CS0246  The type or namespace name 'richTextBoxCodigo' could not be found (are you missing a using directive or an assembly reference?)
Error CS0538  'richTextBoxCodigo' in explicit interface declaration is not an interface
Error CS0501  'Form1.Undo()' must declare a body because it is not marked abstract, extern, or partial
Nota:
Ahora que he copiado la definición de las expresiones lambda en la documentación de C# (para pegarla arriba), al ver lo que en esa documentación llaman lambda de instrucción, veo que ahí se indica que hay que encerrar la expresión entre llaves.

En mi defensa te dirá que la documentación de C# la he leído cuando estaba revisando lo que ya había escrito hasta después del siguiente ejemplo. En fin…
Aunque lo mismo tampoco me hubiese enterado…

¡Qué torpe es el Guille!
La otra solución es definir esa expresión lambda dentro del método que sea, por ejemplo Form1_Load.
En este caso no tenemos que definir el control richTextBoxCodigo como static y la expresión lambda la definimos dentro del evento Load del formulario.
Esto está bien si no necesitamos usar esa expresión lambda imbuida en una variable en algún otro método de esa clase.

Este sería el código de ejemplo:

    private void Form1_Load(object sender, EventArgs e)
    {

        EventHandler lambdaUndo = (sender, e) => 
            {
                if (richTextBoxCodigo.CanUndo) richTextBoxCodigo.Undo();
            };

        // Usando expresión lambda definida previamente
        buttonUndo.Click += lambdaUndo;
        menuUndo.Click += lambdaUndo;

    }
Pero en mi caso en particular esta última solución no me sirve, ya que esa expresión lambda la tenía que usar en varios métodos de la misma clase.

Nota (repetimos):
Al definir los parámetros de la expresión lambda en C# no es necesario indicar el tipo de datos de cada argumento, con indicar los nombres de los identificadores (variables) es suficiente, tal como te muestre en el código anterior.

Y hasta aquí hemos llegado… (el fin)
<br>
<br>
Espero que te sea de utilidad.<br>
Guillermo<br>
<br>
Actualizado el 28 de septiembre de 2020 a las 09:05

