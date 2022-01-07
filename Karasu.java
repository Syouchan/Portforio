import java.awt.Graphics;
import java.awt.image.BufferedImage;

import javax.swing.JFrame;

public class Karasu{
	int karasu_x;
	int karasu_y;
	BufferedImage image;
	int speed;

	Karasu(BufferedImage image){
		karasu_x = 600;
		karasu_y = (int)(Math.random() * 360);
		this.image = image;
		speed = 5;
	}

	void move() {
		karasu_x -= speed;
		if(karasu_x < 0) {
			karasu_x = 600;
		}
	}
	//int fusen_x , int fusen_y等の引数でも良いが、Fusenaオブジェクトを渡
	//してそのフィールド変数を参照する方が望ましいかな？
	boolean isAtari(Fusenman fusenman) {
		if (fusenman.fusen_x < karasu_x + 40 && fusenman.fusen_x + 40 > karasu_x &&
				fusenman.fusen_y < karasu_y + 40 && fusenman.fusen_y + 40 > karasu_y) {

					return true;
				}
		return false;
	}
	void draw(Graphics g, JFrame frame1) {
			g.drawImage(image, karasu_x, karasu_y, frame1);
	}
}