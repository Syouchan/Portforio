Public Class Form1
    Dim x, y, w, h, a, b As Integer
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Timer1.Enabled = True
        x = 20
        y = 20
        w = 20
        h = 20
        a = 20
        b = 30
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Timer1.Enabled = False
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim g As Graphics = CreateGraphics()
        g.Clear(Color.White)
        g.DrawEllipse(Pens.Red, x, y, w, h)

        If x + 3 * a > 300 Then
            a = -a
        End If
        If x < w Then
            a = -a
        End If
        If y + 3 * b > 300 Then
            b = -b
        End If
        If y < h Then
            b = -b
        End If
        x = x + a
        y = y + b
    End Sub
End Class
