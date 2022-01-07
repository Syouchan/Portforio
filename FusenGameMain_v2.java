import java.awt.Color;
import java.awt.Graphics;
import java.awt.event.KeyAdapter;
import java.awt.event.KeyEvent;
import java.awt.image.BufferedImage;
import java.io.IOException;

import javax.imageio.ImageIO;
import javax.swing.JFrame;

public class FusenGameMain_v2 extends Thread{
	JFrame frame1;
	BufferedImage backimage, fusenmanimage, karasuimage, darkfusenmanimage;
	boolean space_flag = false;
	boolean A_flag = false;
	boolean D_flag = false;
	boolean F_flag = false;
	boolean ENTER_flag = false;
	boolean Continue = false;
	int count = 1;
	int zanki1 = 4;
	int zanki = 3;
	int contcount = 0;

	FusenGameMain_v2() {
		frame1 = new JFrame("風船ゲーム");
		frame1.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		frame1.setBackground(Color.WHITE);
		frame1.setResizable(false);
		frame1.setVisible(true);
		frame1.setSize(600, 400);
		frame1.setLocale(null);
		frame1.addKeyListener(new MyKeyAdapter());
		frame1.createBufferStrategy(2);
		frame1.setIgnoreRepaint(true);
		try {
			backimage = ImageIO.read(getClass().getResource("back.jpg"));
			fusenmanimage = ImageIO.read(getClass().getResource("fusenman.png"));
			karasuimage = ImageIO.read(getClass().getResource("karasu.png"));
			darkfusenmanimage = ImageIO.read(getClass().getResource("dark_fusenman.png"));
		}catch(IOException e) {
			e.printStackTrace();
		}
		start();
	}
	public void run() {
		Fusenman fusenman = new Fusenman(100, 200, fusenmanimage);

		int karasu_hikisuu = (int)(Math.random() * 4 + 1 ); //*2
		Karasu[] karasu = new Karasu[karasu_hikisuu];
		for(int i = 0 ; i < karasu.length ; i++) karasu[i] = new Karasu(karasuimage);
		int darkfusen_hikisuu = (int)(Math.random() * 4 + 1 );
		DarkFusen[] darkfusen = new DarkFusen[darkfusen_hikisuu];
		for(int i = 0 ; i < darkfusen.length ; i++) darkfusen[i] = new DarkFusen(darkfusenmanimage);
		int packman_hikisuu = 4;
		Pacman[] pack = new Pacman[packman_hikisuu];
		for(int i = 0 ; i < pack.length ; i++) pack[i] = new Pacman(800, 100);

		boolean gameover = false;
		for(;;) {
			Graphics g = frame1.getGraphics();
			g.drawImage(backimage , 0, 20, frame1);

			g.drawString("残り" + zanki + "回", 530, 50);
			g.drawString( "コンティニュー回数" + contcount + "回", 460, 390);

			fusenman.move(space_flag, A_flag, D_flag, ENTER_flag);
			fusenman.draw(g, frame1);

			for(int i = 0 ; i < karasu.length ; i++) {
				karasu[i].move();
				if(gameover==false) {
					gameover = karasu[i].isAtari(fusenman);
				}karasu[i].draw(g, frame1);
			}
			for(int i = 0 ; i < darkfusen.length ; i++) {
				darkfusen[i].move();
				if(gameover == false) gameover = darkfusen[i].isAtari(fusenman);
				darkfusen[i].draw(g, frame1);
			}
			for(int i = 0 ; i < pack.length ; i++) if(gameover == false)
			gameover = pack[i].drawPacman(g, fusenman.fusen_x, fusenman.fusen_y, frame1);

			if(zanki1 > 0 ) {
			if(gameover == true) {

				g.drawString("残り" + zanki + "回", 300, 200);
				try {
					Thread.sleep(1000);
				}catch (InterruptedException e) {
						e.printStackTrace();
				}
				new Fusenman(100, 200, fusenmanimage);

				for(int i = 0 ; i < karasu.length ; i++) karasu[i] = new Karasu(karasuimage);
				for(int i = 0 ; i < darkfusen.length ; i++) darkfusen[i] = new DarkFusen(darkfusenmanimage);
				zanki1 = zanki1 - 1;
				zanki = zanki - 1;
				gameover = false;
				}
			}else{
					g.drawString("ゲームオーバー", 300, 200);
					g.drawString("コンティニューはF", 100, 200);
					Continue = true;
					if(F_flag == true && count > 0) {
						g.drawString("ロード中", 150, 300);
						try {
							Thread.sleep(1000);
						}catch (InterruptedException e) {
								e.printStackTrace();
						}
						for(int i = 0 ; i < karasu.length ; i++) {
							karasu[i] = new Karasu(karasuimage);
						}
						for(int i = 0 ; i < darkfusen.length ; i++) {
							darkfusen[i] = new DarkFusen(darkfusenmanimage);
						}
						contcount = contcount + 1;
						zanki = 3;
						zanki1 = 4;
						count = count - 1;
						Continue = false;
					}else{

					}
					count = 1;
			}
			try {
				Thread.sleep(30);
			}catch (InterruptedException e ) {
					e.printStackTrace();
			}
		}
	}

	public static void main(String[] args) {
		FusenGameMain_v2 main = new FusenGameMain_v2();
	}
	class MyKeyAdapter extends KeyAdapter{
		public void keyPressed(KeyEvent ev) {
			if(ev.getKeyCode()== KeyEvent.VK_SPACE) space_flag = true;
			else if (ev.getKeyCode() == KeyEvent.VK_A) A_flag = true;
			else if (ev.getKeyCode() == KeyEvent.VK_D) D_flag = true;
			else if (ev.getKeyCode() == KeyEvent.VK_F) F_flag = true;
			else if (ev.getKeyCode() == KeyEvent.VK_ENTER) ENTER_flag = true;
		}
		public void keyReleased(KeyEvent ev) {
			if(ev.getKeyCode() == KeyEvent.VK_SPACE) space_flag = false;
			else if (ev.getKeyCode() == KeyEvent.VK_A) A_flag = false;
			else if (ev.getKeyCode() == KeyEvent.VK_D) D_flag = false;
			else if (ev.getKeyCode() == KeyEvent.VK_F) F_flag = false;
			else if (ev.getKeyCode() == KeyEvent.VK_ENTER) ENTER_flag = false;
		}
	}
}