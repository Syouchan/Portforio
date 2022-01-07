
import javax.swing.JFrame;
import javax.swing.JTextArea;
import javax.swing.JPanel;
import javax.swing.JButton;
import javax.swing.JScrollPane;
import javax.swing.text.BadLocationException;
import java.awt.Container;
import java.awt.BorderLayout;
import java.awt.event.ActionListener;
import java.awt.event.ActionEvent;
import java.awt.Color;

class Br extends JFrame implements ActionListener{
  JTextArea textarea_left;
  JTextArea textarea_right;

  public static void main(String args[]){
    Br frame = new Br("BrEditor");
    frame.setVisible(true);
  }

  Br(String title){
    setTitle(title);
    setBounds(100, 100, 1800, 1080);
    setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);

    textarea_left = new JTextArea(60, 80);
    JScrollPane scrollpane_left = new JScrollPane(textarea_left);
    textarea_right = new JTextArea(60, 80);
    JScrollPane scrollpane_right = new JScrollPane(textarea_right);

    textarea_right.setEditable(false);
    textarea_right.setBackground(Color.LIGHT_GRAY);

    JButton button = new JButton("BrChange");
    button.addActionListener(this);

    JPanel p = new JPanel();
    //p.add(textarea_left);
    p.add(scrollpane_left);
    //p.add(textarea_right);
    p.add(scrollpane_right);
    p.add(button);

    Container contentPane = getContentPane();
    contentPane.add(p, BorderLayout.CENTER);
  }
  public void actionPerformed(ActionEvent e){
    int linecount = textarea_left.getLineCount();

    try{
      StringBuilder sb = new StringBuilder();

      for (int i = 0 ; i < linecount ; i++){
        int start = textarea_left.getLineStartOffset(i);
        int end = textarea_left.getLineEndOffset(i);
        sb.append(/*(i + 1) + ":" + */"<br>" + textarea_left.getText(start, end - start));
      }
      textarea_right.setText(new String(sb));
    }catch(BadLocationException err){
      System.out.println("エラーがでとんじゃい！");
    }
  }
}