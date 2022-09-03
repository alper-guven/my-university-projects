package entities.game.fruits;

import entities.game.Fruit;
import javafx.animation.FadeTransition;
import javafx.scene.image.Image;
import javafx.scene.image.ImageView;
import javafx.scene.layout.Pane;
import javafx.util.Duration;

public class Pineapple extends Fruit {

    public Pineapple(){
        name = "pineapple";
        point = 5;
    }

    @Override
    public void create() {

        width = 157.5;
        height = 179.2;

        fruitImgView = new ImageView(new Image("images/fruits/" + name + ".png", width, height, false, false));

        partLeft = new ImageView(new Image("images/fruits/pieces/" + name + "_part_left.png", width/2, height, false, false));
        partRight = new ImageView(new Image("images/fruits/pieces/" + name + "_part_right.png", width/2, height, false, false));


    }

    @Override
    public void animateSlice(Pane gameCanvas, double cutPosX, double cutPosY) {

    }


    @Override
    public void animateSplash(Pane gameCanvas, double cutPosX, double cutPosY) {
        double splashHeight = 297;
        double splashWidth = 299;

        splash = new ImageView(new Image("images/splash/splash-" + name + ".png", splashWidth, splashHeight, false, false));

        splash.relocate(cutPosX - splashHeight/2, cutPosY - splashWidth/2);
        gameCanvas.getChildren().addAll(splash);

        FadeTransition ft = new FadeTransition(Duration.millis(2500), splash);
        ft.setFromValue(1.0);
        ft.setToValue(0.0);
        ft.setCycleCount(1);
        ft.play();
    }

    @Override
    public void slice() {

    }
}
