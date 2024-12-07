using System;
using System.Windows.Forms;

public static class Prompt
{
    public static string ShowDialog(string text, string caption, string defaultValue = "")
    {
        Form prompt = new Form()
        {
            Width = 400,
            Height = 150,
            Text = caption
        };
        Label lblText = new Label() { Left = 10, Top = 10, Text = text };
        TextBox txtInput = new TextBox() { Left = 10, Top = 40, Width = 360, Text = defaultValue };
        Button btnOk = new Button() { Text = "OK", Left = 300, Width = 75, Top = 70, DialogResult = DialogResult.OK };

        prompt.Controls.Add(lblText);
        prompt.Controls.Add(txtInput);
        prompt.Controls.Add(btnOk);
        prompt.AcceptButton = btnOk;

        return prompt.ShowDialog() == DialogResult.OK ? txtInput.Text : string.Empty;
    }
}
