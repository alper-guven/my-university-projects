package entities.game.fruits;

import entities.game.Fruit;
import javafx.animation.FadeTransition;
import javafx.scene.image.Image;
import javafx.scene.image.ImageView;
import javafx.scene.layout.Pane;
import javafx.scene.shape.Line;
import javafx.util.Duration;

public class Apple extends Fruit {

    public Apple(){
        name = "apple";
        point = 2;
    }

    @Override
    public void slice() {

    }

    @Override
    public void create() {

        width = 128;
        height = 114.5;

        fruitImgView = new ImageView(new Image("images/fruits/" + name + ".png", width, height, false, false));

        partLeft = new ImageView(new Image("images/fruits/pieces/" + name + "_part_left.png", width/2, height, false, false));
        partRight = new ImageView(new Image("images/fruits/pieces/" + name + "_part_right.png", width/2, height, false, false));


    }

    @Override
    public void animateSlice(Pane gameCanvas, double cutPosX, double cutPosY) {

//        Line blackLine = Line.create()
//                .startX(170)
//                .startY(30)
//                .endX(20)
//                .endY(140)
//                .fill(Color.BLACK)
//                .strokeWidth(10.0f)
//                .translateY(20)
//                .build();
//
//        gameCanvas.getChildren().add(line);

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
}
