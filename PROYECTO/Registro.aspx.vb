Imports System.Data.SqlClient

Public Class Registro
    Inherits System.Web.UI.Page

    Protected Function RegistrarUsuario(usuario As Usuario) As Boolean

        Try
            Dim helper As New DatabaseHelper()
            Dim query As String = "INSERT INTO USUARIOS (EMAIL, CONTRASEÑA, NOMBRE, APELLIDOS) VALUES (@Email, @Password, @Nombre, @Apellido);"
            Dim parametros As New List(Of SqlParameter) From {
                New SqlParameter("@Email", usuario.Email),
                New SqlParameter("@Password", usuario.Password),
                New SqlParameter("@Nombre", usuario.Nombre),
                New SqlParameter("@Apellido", usuario.Apellido)
            }

            Dim resultado As Boolean = helper.ExecuteNonQuery(query, parametros)
            Return resultado
        Catch ex As Exception
            ' Manejar la excepción según sea necesario
            Return False
        End Try
    End Function

    Protected Sub btnRegistrar_Click(sender As Object, e As EventArgs)
        Dim usuario As New Usuario() With {
    .Email = txtEmail.Text,
    .Password = txtPass.Text,
    .Nombre = txtNombre.Text,
    .Apellido = txtApellido.Text
}

        If RegistrarUsuario(usuario) Then
            ' Redirigir al login o a la página de inicio

            ScriptManager.RegisterStartupScript(
        Me, Me.GetType(),
        "RegistrarUsuarioOk",
        "Swal.fire('Usuario Registrado').then((result) => {
            if (result.isConfirmed) {
                window.location.href = 'Login.aspx';
            }
        });",
        True)

        Else
            ScriptManager.RegisterStartupScript(
        Me, Me.GetType(),
        "RegistrarUsuarioError",
        "Swal.fire('Error al registrar el usuario. Inténtalo de nuevo.');",
        True)
            lblError.Text = "Error al registrar el usuario. Inténtalo de nuevo."
            lblError.Visible = True
        End If

    End Sub
End Class