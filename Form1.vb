Imports System
Imports System.Windows.Forms

Public Class Form1 : Inherits Form

    Shared Sub Main()
    End Sub

    Private WithEvents buttonAbrir As New Button()
    Private WithEvents richTextBoxCodigo As New RichTextBox()
    Private WithEvents buttonUndo As New Button()
    Private WithEvents menuUndo As New ToolStripMenuItem()

    Private lambdaUndo As EventHandler =
                Sub(sender As Object, e As EventArgs) _
                    If richTextBoxCodigo.CanUndo Then richTextBoxCodigo.Undo()

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        ' Sin usar expresión lambda
        AddHandler buttonAbrir.Click, AddressOf buttonAbrir_Click
        ' Usando expresión lambda
        AddHandler buttonAbrir.Click, Sub() Abrir()

        ' Usando expresión lambda definida previamente
        AddHandler buttonUndo.Click, lambdaUndo
        AddHandler menuUndo.Click, lambdaUndo

        ' Usando expresión lambda no definida previamente
        AddHandler buttonUndo.Click,
                     Sub() If richTextBoxCodigo.CanUndo Then richTextBoxCodigo.Undo()
        AddHandler menuUndo.Click,
                     Sub() If richTextBoxCodigo.CanUndo Then richTextBoxCodigo.Undo()

    End Sub

    Private Sub buttonAbrir_Click(sender As Object, e As EventArgs) Handles buttonAbrir.Click
        Abrir()
    End Sub

    Private Sub variosUndo_Click(sender As Object, e As EventArgs) _
                       Handles buttonUndo.Click, menuUndo.Click
        If richTextBoxCodigo.CanUndo Then richTextBoxCodigo.Undo()
    End Sub

    Private Sub Abrir()
    End Sub

End Class