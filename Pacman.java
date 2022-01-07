import java.awt.Color;
import java.awt.Frame;
import java.awt.Graphics;


public class Pacman {

	private int pacman_x;
	private int pacman_y;
	private int time = 0;

	Pacman(int x , int y){
		pacman_x = x;
		pacman_y = y;
	}

	public boolean drawPacman(Graphics g ,int fusen_x , int fusen_y , Frame frame1){
		boolean game_over = false;

		//あたり判定

		if (fusen_x < pacman_x + 40 && fusen_y + 40 > pacman_x &&
				fusen_y < pacman_y + 40 && fusen_y + 40 > pacman_y) { // 48を40ぐらいにするといいかも
			game_over = true;
		}



		//動き
		pacman_x = pacman_x - 3;

		if ( pacman_x < 0){
			pacman_x =  (int)(Math.random() * 400) + 600;
			pacman_y =  (int)(Math.random() * 400);
		}

		//描画
		//g.drawImage(karasu_image, karasu_x, karasu_y, frame1);
		if(time % 20 < 10){
			g.setColor(Color.yellow);
			g.fillOval(pacman_x, pacman_y, 48, 48);
			g.setColor(Color.black);
			g.fillOval(pacman_x+20, pacman_y+10, 10, 10);
		}else{
			g.setColor(Color.yellow);
			g.fillArc(pacman_x, pacman_y, 48, 48, 220, 290);
			g.setColor(Color.black);
			g.fillOval(pacman_x+20, pacman_y+10, 10, 10);
		}
		time ++;


		return game_over;
	}


}
