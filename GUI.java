
import javax.swing.JButton;
import javax.swing.JFrame;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import javax.swing.JTextField;

public class GUI extends JFrame implements ActionListener
{
	Calculator hen = new Calculator();
	public GUI(String s,int w, int h)
	{
		this.setTitle(s);
		this.setSize(w, h);
		this.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		this.setResizable(false);
		this.setLayout(null);
		this.setVisible(true);
		this.setIgnoreRepaint(true);
		this.createBufferStrategy(2);
		int init_x = 20;
		int init_y = 80;
		int size = 50;
		int space = 10;
		int x = init_x;
		int y = init_y;
		int count = 0;

		JTextField field = new JTextField();
		field.setBounds(20, 20, 230, 50);
		this.add(field);

		JButton zero = new JButton("0");
		zero.setBounds(20,260,110,50);
		this.add(zero);
		zero.addActionListener(this);

		JButton iko_ru = new JButton("=");
		iko_ru.setBounds(140,260,50,50);
		this.add(iko_ru);
		iko_ru.addActionListener(this);

		JButton tasu = new JButton("+");
		tasu.setBounds(200,80,50,50);
		this.add(tasu);
		tasu.addActionListener(this);

		JButton hiku = new JButton("-");
		hiku.setBounds(200,140,50,50);
		this.add(hiku);
		hiku.addActionListener(this);

		JButton kakeru = new JButton("ｘ");
		kakeru.setBounds(200,200,50,50);
		this.add(kakeru);
		kakeru.addActionListener(this);

		JButton waru = new JButton("÷");
		waru.setBounds(200,260,50,50);
		this.add(waru);
		waru.addActionListener(this);

		JButton Delete = new JButton("Del");
		Delete.setBounds(20,320,230,50);
		this.add(Delete);
		Delete.addActionListener(this);

		//0-9のボタン
		JButton[] GG = new JButton[9];

		for(int i = 0 ; i < 3 ; i++)
		{
			for(int j = 0 ; j < 3 ; j++)
			{
				GG[count] = new JButton(Integer.toString(count + 1));
				GG[count].setBounds(x, y, size, size);
				this.add(GG[count]);
				GG[count].addActionListener(this);
				x += space +size;
				count++;

			}
			y += space + size;
			x = init_x;
		}
	}

	public static void main(String[] args)
	{
		new GUI("ケイサンキ", 290, 420);
	}

	public void actionPerformed(ActionEvent e)
	{
		//戻り値のStringには押されたボタンの文字列が格納される
		String cmd = e.getActionCommand();
		for(int i=0;i<10;i++)
		{
			if(cmd.equals(String.valueOf(i)))
			{
				System.out.println(i);
			}
		}
		if(cmd.equals("="))
		{
			System.out.println("=");
		}
		else if(cmd.equals("+"))
		{
			System.out.println("+");
		}
		else if(cmd.equals("-"))
		{
			System.out.println("-");
		}
		else if(cmd.equals("ｘ"))
		{
			System.out.println("ｘ");
		}
		else if(cmd.equals("÷"))
		{
			System.out.println("÷");
		}
		else if(cmd.equals("Del"))
		{
			System.out.println("Del");
		}
	}
}
