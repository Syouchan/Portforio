
import java.awt.Graphics;
import java.awt.image.BufferedImage;

import javax.swing.JFrame;

public class DarkFusen {
	int darkfusen_x;
	int darkfusen_y;
	BufferedImage image;
	boolean darkfusen_flag;
	int speed;

	DarkFusen(BufferedImage image){
		darkfusen_x = 550;
		darkfusen_y = (int)(Math.random() * 360) ;
		darkfusen_flag = true;
		this.image = image;
		speed = 10;
	}

	void move() {
		darkfusen_x -= speed;
		if(darkfusen_x < 0){
			darkfusen_x = 600;
		}
		if(darkfusen_flag == true){
			darkfusen_y += speed;
		}
		else{
			darkfusen_y -= speed;
		}

		if(darkfusen_y >= 350){
			darkfusen_flag = false;
			darkfusen_y = 349;
		}
		else if(darkfusen_y <= 20){
			darkfusen_flag = true;
			darkfusen_y = 21;
		}
	}
	boolean isAtari(Fusenman fusenman) {
		if (fusenman.fusen_x < darkfusen_x + 40 && fusenman.fusen_x + 40 > darkfusen_x &&
				fusenman.fusen_y < darkfusen_y + 40 && fusenman.fusen_y + 40 > darkfusen_y) {
					return true;
				}
		return false;
	}
	void draw(Graphics g, JFrame frame1) {
		g.drawImage(image , darkfusen_x , darkfusen_y , frame1);
	}
}
