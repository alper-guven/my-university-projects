package controllers;

import entities.GameRecord;
import entities.User;
import entities.game.Bomb;
import entities.game.Fruit;
import entities.game.bombs.BombX;
import entities.game.fruits.*;
import javafx.animation.KeyFrame;
import javafx.animation.Timeline;
import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.scene.Scene;
import javafx.scene.control.Button;
import javafx.scene.control.Label;
import javafx.scene.control.TableColumn;
import javafx.scene.control.TableView;
import javafx.scene.control.cell.PropertyValueFactory;
import javafx.scene.image.Image;
import javafx.scene.layout.*;
import javafx.scene.paint.Color;
import javafx.scene.text.Font;
import javafx.stage.Stage;
import javafx.util.Duration;
import models.GameRecordDao;

import java.sql.Date;
import java.sql.SQLException;
import java.util.Calendar;
import java.util.Timer;
import java.util.concurrent.ThreadLocalRandom;


public class Game {

    private GameRecordDao gameRecordDao;
    private User player;

    private int score = 0;
    private double gameTime;
    private long gameStartTime;
    private int remainingMissedFruits = 3;


    private Stage gameWindow;
    private Scene gameScene;
    private Scene gameLobby;
    private Pane gameLobbyCanvas;
    private Pane gameCanvas;

    private Label lblPoint;
    private Label lblRemainingMissedFruits;
    private Label lblDuration;

    private Button btnPause;
    private Button btnContiunie;
    private Button btnBackToLobby;

    private double screenWidth;
    private double screenHeight;

    private Timeline gameProcess;
    private Timeline periodProcess;
    private boolean isPeriodOngoing;

    private String gameState;

    private TableView<GameRecord> leaderboard = new TableView<>();
    private ObservableList leaderboardData = FXCollections.observableArrayList();

    private TableView<GameRecord> personalboard = new TableView<>();
    private ObservableList personalboardData = FXCollections.observableArrayList();

    private final ThreadLocalRandom rand;

    {
        rand = ThreadLocalRandom.current();
    }

    public Game(Stage gameStage, Scene gameLobbyScene, Pane lobbyCanvas, User user){

        player = user;
        gameRecordDao = new GameRecordDao();

        gameWindow = gameStage;
        gameLobby = gameLobbyScene;
        gameLobbyCanvas = lobbyCanvas;

        screenWidth = gameWindow.getWidth();
        screenHeight = gameWindow.getHeight();

//        long l = System.currentTimeMillis();
//
//        System.out.println(l);

        System.out.println("W: "+ screenWidth + " " + "H:" + screenHeight);

        createGameCanvas();

    }

    public void play(){

        gameWindow.setScene(gameScene);
        gameWindow.setFullScreen(true);

        gameStartTime = System.currentTimeMillis();

        gameState = "live";
        double periodCheckDuration = 1.0;

        gameProcess = new Timeline(new KeyFrame(Duration.seconds(periodCheckDuration), e -> {

            if(gameState.equals("live")){

                if(remainingMissedFruits <= 0){
                    gameState = "finished";
                    endGame();
                }else{
                    updateDuration();
                    if (!isPeriodOngoing){
                        launchFruitPeriod();
                    }
                }

            }else{
                gameProcess.stop();
            }

        }));
        gameProcess.setCycleCount(Timeline.INDEFINITE);
        gameProcess.play();



    }

    private void updateDuration() {

        gameTime = (System.currentTimeMillis() - gameStartTime) / 1000;

        lblDuration.setText( "Duration: " + gameTime );

    }

    private void launchFruitPeriod(){

        int periodLength = rand.nextInt(7 - 4) + 4;
        int numberOfFruits = rand.nextInt(6 - 1) + 1;


        double periodDuration = 1.0;

        int[] numberOfFruitsToThrowOnSecond = new int[periodLength];

        for (int i = 0, k = numberOfFruits; k != 0 ; i++) {

            if(i == periodLength-1){
                numberOfFruitsToThrowOnSecond[i] = k;
                k = 0;
            }else {
                numberOfFruitsToThrowOnSecond[i] = rand.nextInt(k);
                k -= numberOfFruitsToThrowOnSecond[i];
            }

        }

        var wrapper = new Object(){

            int w = 0;

            int getNumOfFruitsToThrow(){
                int res = numberOfFruitsToThrowOnSecond[w];
                w++;
                return res;
            }

            boolean doesIncludesBomb(){
                boolean includesBomb = false;
                if( rand.nextInt(20 - 1) + 1 == 1 ){
                    includesBomb = true;
                }
                return includesBomb;
            }

        };

        periodProcess = new Timeline(new KeyFrame(Duration.seconds(periodDuration), e -> {

            if(gameState.equals("live") ){
                for (int i = wrapper.getNumOfFruitsToThrow()-1; 0 < i; i --) {
                    Fruit newFruit = createRandomFruit();
                    throwFruit(newFruit);
                }

                if( wrapper.w == Math.floor(periodLength/2) && wrapper.doesIncludesBomb()){
                    Bomb newBomb = createBomb();
                    throwBomb(newBomb);
                }

            }else{
                periodProcess.stop();
            }

        }));

        periodProcess.setCycleCount(periodLength);

        periodProcess.setOnFinished( e -> {
            isPeriodOngoing = false;
        });

        periodProcess.play();


//        newFruit.fruitImgView.relocate( screenWidth/10,  screenHeight - 200 );
    }

    private void throwBomb(Bomb bomb) {

        final ThreadLocalRandom rand = ThreadLocalRandom.current();

        double entryHigh =  screenWidth *0.80;
        double entryLow = screenWidth *0.20;

        double entryX = rand.nextDouble( entryHigh - entryLow  ) + entryLow;

        bomb.bombImageView.relocate(entryX, screenHeight);
        gameCanvas.getChildren().add(bomb.bombImageView);

        double duration = 0.0166;

        double exitHigh = entryX + screenWidth *0.20 - bomb.width;
        double exitLow = entryX - screenWidth * 0.20 - bomb.width;

        double exitPosX = rand.nextDouble( exitHigh - exitLow  ) + exitLow;

        double topHigh = screenHeight * 0.35;
        double topLow = screenHeight * 0.20;
        double topY = rand.nextDouble( topHigh - topLow) + topLow;

        int cycleCount = 60;

        var wrapper = new Object(){

            double x = entryX;
            double y = screenHeight;
            Timeline throwTimer;
            double currCycleNo = 1;
            double cycleCountdown = cycleCount;
            double sumOfCycleNumbers = cycleCount*(cycleCount+1) / 2;

            double nextX(){
                x += ((exitPosX-entryX)/2)/ cycleCount;
                return x;
            }

            double nextY(){
                y -= ((screenHeight-topY) * (cycleCountdown / sumOfCycleNumbers));
                return y;
            }

            void updateCycleCounter(){
                currCycleNo++;
                cycleCountdown--;
            }

        };

        wrapper.throwTimer = new Timeline(new KeyFrame(Duration.seconds(duration), event -> {

            wrapper.updateCycleCounter();

            if(gameState.equals("live")){
                bomb.bombImageView.relocate(wrapper.nextX(), wrapper.nextY());
            }else{
                wrapper.throwTimer.stop();
            }

        }));
        wrapper.throwTimer.setCycleCount(cycleCount);
        wrapper.throwTimer.setOnFinished( e -> {
                if (gameState.equals("live")){
                    fallBomb(bomb, wrapper.x, wrapper.y, exitPosX);
                }
        } );
        wrapper.throwTimer.play();

    }

    private void fallBomb(Bomb bomb, double lastX, double lastY, double exitPosX) {
        double duration = 0.0166;
        int cycleCount = 60;

        var wrapper = new Object(){

            double x = lastX;
            double y = lastY;
            Timeline fallTimer;

            double currCycleNo = 1;
            double cycleCountdown = cycleCount;
            double sumOfCycleNumbers = cycleCount*(cycleCount+1) / 2;

            double nextX(){
                x -= (lastX- exitPosX)/cycleCount;
                return x;
            }

            double nextY(){
                y += (screenHeight-lastY) * (currCycleNo / sumOfCycleNumbers);
                return y;
            }

            void updateCycleCounter(){
                currCycleNo++;
                cycleCountdown--;
            }

        };

        wrapper.fallTimer = new Timeline(new KeyFrame(Duration.seconds(duration), event -> {

            wrapper.updateCycleCounter();

            if( gameState.equals("live") ){
                bomb.bombImageView.relocate(wrapper.nextX(), wrapper.nextY());
            }else{
                wrapper.fallTimer.stop();
            }

        }));
        wrapper.fallTimer.setCycleCount(cycleCount);
        wrapper.fallTimer.setOnFinished( e -> {
                gameCanvas.getChildren().remove(bomb.bombImageView);
        } );

        wrapper.fallTimer.play();

    }

    private Bomb createBomb() {

        Bomb bomb = new BombX();
        bomb.create();

        bomb.bombImageView.setOnMouseDragEntered( dragEntered -> {

            bomb.explode(gameCanvas, bomb.bombImageView.getLayoutX(), bomb.bombImageView.getLayoutY());

//            gameCanvas.getChildren().remove(bomb.bombImageView);
            gameState = "finished";
            isPeriodOngoing = false;

            Timeline timeline = new Timeline(new KeyFrame(
                    Duration.millis(2000),
                    ae -> endGame()));
            timeline.play();

        });

        return bomb;
    }

    private Fruit createRandomFruit() {
        int fruitPick = rand.nextInt(5 - 1) + 1;

        Fruit newFruit;

        switch (fruitPick) {

            case 1:
                newFruit = new Apple();
                break;
            case 2:
                newFruit = new Orange();
                break;
            case 3:
                newFruit = new Pear();
                break;
            case 4:
                newFruit = new Pineapple();
                break;
            case 5:
                newFruit = new Strawberry();
                break;

            default:
                newFruit = new Apple();
        }


        newFruit.create();

        newFruit.fruitImgView.setOnMouseDragEntered( dragEntered -> {
            System.out.println("Meyve kesildi");
            newFruit.isCut = true;
            increaseScore(newFruit.point);

            newFruit.animateSplash(gameCanvas, newFruit.fruitImgView.getLayoutX(), newFruit.fruitImgView.getLayoutY());
            newFruit.animateSlice(gameCanvas, newFruit.fruitImgView.getLayoutX(), newFruit.fruitImgView.getLayoutY());
            gameCanvas.getChildren().remove(newFruit.fruitImgView);


//            setFruitToNull(newFruit);
        });

        newFruit.fruitImgView.setOnMouseDragExited( dragExited -> {
            System.out.println("cikti");
        });

        return newFruit;
    }

    private void throwFruit(Fruit fruit){

        final ThreadLocalRandom rand = ThreadLocalRandom.current();

        double entryHigh =  screenWidth *0.80;
        double entryLow = screenWidth *0.20;

        double entryX = rand.nextDouble( entryHigh - entryLow  ) + entryLow;

        fruit.fruitImgView.relocate(entryX, screenHeight);
        gameCanvas.getChildren().add(fruit.fruitImgView);

        double duration = 0.0166;

        double exitHigh = entryX + screenWidth *0.20 - fruit.width;
        double exitLow = entryX - screenWidth * 0.20 - fruit.width;

        double exitPosX = rand.nextDouble( exitHigh - exitLow  ) + exitLow;

        double topHigh = screenHeight * 0.35;
        double topLow = screenHeight * 0.20;
        double topY = rand.nextDouble( topHigh - topLow) + topLow;

        int cycleCount = 60;

        var wrapper = new Object(){

            double x = entryX;
            double y = screenHeight;
            Timeline throwTimer;
            double currCycleNo = 1;
            double cycleCountdown = cycleCount;
            double sumOfCycleNumbers = cycleCount*(cycleCount+1) / 2;

            double nextX(){
                x += ((exitPosX-entryX)/2)/ cycleCount;
                return x;
            }

            double nextY(){
                y -= ((screenHeight-topY) * (cycleCountdown / sumOfCycleNumbers));
                return y;
            }

            void updateCycleCounter(){
                currCycleNo++;
                cycleCountdown--;
            }

        };

        wrapper.throwTimer = new Timeline(new KeyFrame(Duration.seconds(duration), event -> {

            wrapper.updateCycleCounter();

            if(gameState.equals("live")){
                if( !fruit.isCut){
                    fruit.fruitImgView.relocate(wrapper.nextX(), wrapper.nextY());
                }else{

                    fallFruitParts(fruit, wrapper.x, wrapper.y);

                    wrapper.throwTimer.stop();

                }
            }else{
                gameCanvas.getChildren().remove(fruit.fruitImgView);
                wrapper.throwTimer.stop();
            }

        }));
        wrapper.throwTimer.setCycleCount(cycleCount);
        wrapper.throwTimer.setOnFinished( e -> {
            if ( !fruit.isCut ){
                fallFruit(fruit, wrapper.x, wrapper.y, exitPosX);
            }
        } );
        wrapper.throwTimer.play();
    }

    private void fallFruit(Fruit fruit, double lastX, double lastY, double exitPosX){

        double duration = 0.0166;
        int cycleCount = 60;

        var wrapper = new Object(){

            double x = lastX;
            double y = lastY;
            Timeline fallTimer;

            double currCycleNo = 1;
            double cycleCountdown = cycleCount;
            double sumOfCycleNumbers = cycleCount*(cycleCount+1) / 2;

            double nextX(){
                x -= (lastX- exitPosX)/cycleCount;
                return x;
            }

            double nextY(){
                y += (screenHeight-lastY) * (currCycleNo / sumOfCycleNumbers);
                return y;
            }

            void updateCycleCounter(){
                currCycleNo++;
                cycleCountdown--;
            }

        };

        wrapper.fallTimer = new Timeline(new KeyFrame(Duration.seconds(duration), event -> {

            wrapper.updateCycleCounter();

            if( !fruit.isCut ){
                fruit.fruitImgView.relocate(wrapper.nextX(), wrapper.nextY());
            }else{
                fallFruitParts(fruit, wrapper.x, wrapper.y);
                wrapper.fallTimer.stop();
            }

        }));
        wrapper.fallTimer.setCycleCount(cycleCount);
        wrapper.fallTimer.setOnFinished( e -> {
            if ( !fruit.isCut ){
                gameCanvas.getChildren().remove(fruit.fruitImgView);
                setFruitToNull(fruit);
                updateRemainingMissedFruits();
            }
        } );

        wrapper.fallTimer.play();
    }

    private void fallFruitParts(Fruit fruit, double cutPosX, double cutPosY){

        double leftFirstX = cutPosX - (fruit.width*0.25);
        double rightFirstX = cutPosX + (fruit.width*0.25);

        fruit.partLeft.relocate( leftFirstX, cutPosY );
        fruit.partLeft.setRotate(-20);
        fruit.partRight.relocate( rightFirstX, cutPosY );
        fruit.partRight.setRotate(20);
        gameCanvas.getChildren().addAll( fruit.partLeft, fruit.partRight );

        double duration = 0.0166;
        int cycleCount = 60;

        var wrapper = new Object(){

            double leftX = leftFirstX;
            double rightX = rightFirstX;
            double y = cutPosY;

            double widthChangePerMove = (screenWidth * 0.1)/cycleCount;

            Timeline partsFallTimer;

            double currCycleNo = 1;
            double cycleCountdown = cycleCount;
            double sumOfCycleNumbers = cycleCount*(cycleCount+1) / 2;

            double nextLeftX(){
                leftX -= widthChangePerMove;
                return leftX;
            }

            double nextRightX(){
                rightX += widthChangePerMove;
                return rightX;
            }

            double nextY(){
                y += (screenHeight-cutPosY) * (currCycleNo / sumOfCycleNumbers);
                return y;
            }

            void updateCycleCounter(){
                currCycleNo++;
                cycleCountdown--;
            }

        };

        wrapper.partsFallTimer = new Timeline(new KeyFrame(Duration.seconds(duration), event -> {

            wrapper.updateCycleCounter();

            fruit.partLeft.relocate(wrapper.nextLeftX(), wrapper.nextY());
            fruit.partRight.relocate(wrapper.nextRightX(), wrapper.nextY());

        }));
        wrapper.partsFallTimer.setCycleCount(cycleCount);
        wrapper.partsFallTimer.setOnFinished( e -> {
            gameCanvas.getChildren().removeAll(fruit.partRight, fruit.partLeft);
        } );
        wrapper.partsFallTimer.play();


    } // END OF fallFruitParts()

    private void increaseScore(int fruitPoint){

        score += fruitPoint;

        lblPoint.setText( Integer.toString(score) );

    }

    private void updateRemainingMissedFruits(){
        remainingMissedFruits -= 1;
        lblRemainingMissedFruits.setText( Integer.toString(remainingMissedFruits) );
    }

    private void pauseGame(){
        gameState = "paused";
        periodProcess.pause();
        gameProcess.pause();
    }

    private void continueGame(){
        gameState = "live";
        gameProcess.play();
        periodProcess.play();
    }

    private void endGame(){
        gameState = "finished";
        periodProcess.stop();
        gameProcess.stop();

        gameCanvas.getChildren().remove(btnPause);

        java.sql.Date time1 = new java.sql.Date(Calendar.getInstance().getTime().getTime());

        System.out.println( player.getId() + " " + time1 + " " +(float) gameTime + " " +score);

//        var wrapper = new Object(){
//            public Date time = time1;
//            public int playerID = player.getId();
//        };

        GameRecord newGameRecord = new GameRecord(player.getId(), time1, (float) gameTime, score);

        gameRecordDao.insertGameRecord(newGameRecord);

        printLeaderboard();
        printPersonalBoard();

    }

    private void setFruitToNull(Fruit fruitX){
        fruitX = null;
    }

    private void createGameCanvas(){

        Pane canvas = new Pane();
//        canvas.setPrefSize(screenWidth,screenHeight);

        BackgroundImage myBI= new BackgroundImage(new Image("images/game-bg.jpg", screenWidth, screenHeight,false,true),
                BackgroundRepeat.NO_REPEAT, BackgroundRepeat.NO_REPEAT, BackgroundPosition.DEFAULT,
                BackgroundSize.DEFAULT);
        canvas.setBackground(new Background(myBI));


        btnPause = new Button("Pause");
        btnPause.relocate(screenWidth - 250, 20);
        btnPause.setOnAction(e->{
            gameState = "paused";
            pauseGame();
            gameCanvas.getChildren().remove(btnPause);
            gameCanvas.getChildren().add(btnContiunie);
        });

        btnContiunie = new Button("Continue");
        btnContiunie.relocate(screenWidth - 250, 20);
        btnContiunie.setOnAction(e->{
            gameState = "live";
            continueGame();
            gameCanvas.getChildren().add(btnPause);
            gameCanvas.getChildren().remove(btnContiunie);

        });

        // btnBackToLobby
        btnBackToLobby = new Button("Back to Lobby");
        btnBackToLobby.relocate(screenWidth - 400, 20);
        btnBackToLobby.setOnAction(e->{
            killGame();
        });



        lblPoint = new Label("0");
        lblPoint.setFont(Font.font ("Verdana", 20));
        lblPoint.setTextFill(Color.WHITE);
        lblPoint.relocate( 50,20);

        // lblDuration

        lblDuration = new Label("Duration: 0");
        lblDuration.setFont(Font.font ("Verdana", 20));
        lblDuration.setTextFill(Color.WHITE);
        lblDuration.relocate( 150,20);

        lblRemainingMissedFruits = new Label("3");
        lblRemainingMissedFruits.setFont(Font.font ("Verdana", 20));
        lblRemainingMissedFruits.setTextFill(Color.WHITE);
        lblRemainingMissedFruits.relocate( screenWidth - 50, 20);

        canvas.getChildren().addAll( lblPoint, lblRemainingMissedFruits, lblDuration, btnPause, btnBackToLobby );

        canvas.setOnDragDetected(mouseEvent -> {
            canvas.startFullDrag();

        });
        canvas.setOnMouseDragEntered(mouseEvent -> System.out.println("pane drag entered" ));
        canvas.setOnMouseDragReleased(mouseEvent -> System.out.println("pane drag released"));

        gameCanvas = canvas;

        gameScene = new Scene(canvas, screenWidth, screenHeight);



    }

    private void killGame() {
        gameState = "terminated";
        periodProcess.stop();
        gameProcess.stop();

        printPersonalBoard();
        gameWindow.setScene(gameLobby);
    }

    private void printLeaderboard(){

        leaderboard.setEditable(true);
        //Create Table columns
        TableColumn<GameRecord, String> colUsername = new TableColumn("Username");
        colUsername.setCellValueFactory(new PropertyValueFactory<GameRecord, String>("username"));

        TableColumn<GameRecord, Integer> colDuration = new TableColumn("Duration");
        colDuration.setCellValueFactory(new PropertyValueFactory<GameRecord, Integer>("duration"));

        TableColumn<GameRecord, Integer> colPoint = new TableColumn("Point");
        colPoint.setCellValueFactory(new PropertyValueFactory<GameRecord, Integer>("point"));

        leaderboard.getColumns().addAll(colUsername, colDuration, colPoint);
        //Load data to the table view

        leaderboardData = FXCollections.observableArrayList(gameRecordDao.getLeaderboard());

        leaderboard.setItems(leaderboardData);
        leaderboard.relocate(500,100);
        gameCanvas.getChildren().add(leaderboard);

    }

    private void printPersonalBoard(){

        personalboard.setEditable(true);
        //Create Table columns
        TableColumn<GameRecord, String> colUsername = new TableColumn("Username");
        colUsername.setCellValueFactory(new PropertyValueFactory<GameRecord, String>("username"));

        TableColumn<GameRecord, Integer> colDuration = new TableColumn("Duration");
        colDuration.setCellValueFactory(new PropertyValueFactory<GameRecord, Integer>("duration"));

        TableColumn<GameRecord, Integer> colPoint = new TableColumn("Point");
        colPoint.setCellValueFactory(new PropertyValueFactory<GameRecord, Integer>("point"));

        personalboard.getColumns().addAll(colUsername, colDuration, colPoint);
        //Load data to the table view

        personalboardData = FXCollections.observableArrayList(gameRecordDao.getPreviousGamesOfUser(player));

        personalboard.setItems(personalboardData);
        personalboard.relocate(500,100);
        if( ! gameLobbyCanvas.getChildren().contains(personalboard))
                    gameLobbyCanvas.getChildren().add(personalboard);

    }

}
