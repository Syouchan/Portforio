import java.awt.Graphics;
import java.awt.image.BufferedImage;

import javax.swing.JFrame;

public class Fusenman {
	int fusen_x;
	int fusen_y;
	BufferedImage image;
	int kaisuu = 3;
	boolean skill = true;

	Fusenman(int x, int y, BufferedImage image){
		fusen_x = x;
		fusen_y = y;
		this.image = image;
	}

	void move(boolean space_flag ,boolean A_flag ,boolean D_flag, boolean ENTER_flag) {
		if(fusen_y >= 400 - 48)fusen_y = 400 - 48 ;
		else if(fusen_y < 0 + 20)fusen_y = 0 + 28 ;
		if(fusen_x >= 600 - 48)fusen_x = 600 - 48 ;
		else if(fusen_x < 0 + 20)fusen_x = 0 + 28;
		if(!space_flag) fusen_y = fusen_y + 5;
		else fusen_y = fusen_y - 5;
		if (!A_flag) fusen_x = fusen_x + 5;
		if (!D_flag) fusen_x = fusen_x - 5;
		if(ENTER_flag && skill == true) {fusen_x =fusen_x + 200;
		if(kaisuu < 0) skill = false;
		else kaisuu = kaisuu - 1;
		}
	}
	void draw(Graphics g, JFrame frame1) {
		g.drawImage(image, fusen_x, fusen_y, frame1);
	}
}
