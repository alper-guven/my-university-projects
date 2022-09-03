package entities.game.bombs;

import entities.game.Bomb;
import javafx.animation.FadeTransition;
import javafx.scene.image.Image;
import javafx.scene.image.ImageView;
import javafx.scene.layout.Pane;
import javafx.util.Duration;

public class BombX extends Bomb {

    @Override
    public void slice() {

    }


    @Override
    public void explode(Pane gameCanvas, double cutPosX, double cutPosY) {
        
        gameCanvas.getChildren().remove(this.bombImageView);

        double explosionHeight = 800.00;
        double explosionWidth = 758.00;

        bombExplotionImg = new ImageView(new Image("images/explosion.png", explosionHeight, explosionWidth, false, false));

        bombExplotionImg.relocate(cutPosX - explosionHeight/2, cutPosY - explosionWidth/2);
        gameCanvas.getChildren().add(bombExplotionImg);

        FadeTransition ft = new FadeTransition(Duration.millis(2000), bombExplotionImg);
        ft.setFromValue(1.0);
        ft.setToValue(0.0);
        ft.setCycleCount(1);
        ft.play();
    }

    @Override
    public void create() {

        width = 256.00;
        height = 256.00;

        double resizeRate = 0.75;


        bombImageView = new ImageView(new Image("images/bomb.png", width*resizeRate, height*resizeRate, false, false));
    }
}
