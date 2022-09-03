/*
 *   Author: Alper Güven
 *   Date: March 2019
 *   This program is written for a project given at
 *   BZ 206 PROBABILITY THEORY AND STATISTICS course of Erciyes University / Faculty of Engineering / Computer Engineering
 *
 *   This project aims to see how Monte Carlo Simulation works.
 *
 *   You have N (5 in this example) ordered boxes. Take order number as i, each box has i+1 Black and i+4 White marbles at start.
 *   Starting from first box, blindly pick a marble from a box, and put the picked marble to next box.
 *   Hold the first picked marble's color.
 *   Repeat this till the last box and then, apply the same thing backwards starting from the last box.
 *   When you reach back to first box, pick a marble.
 *   If that marble is White and first picked marble's color is Black, that's a hit!
 *   Our purpose is to calculate probability of this condition.
 *
 *   To execute a Marco Polo Simulation, use simulateMarcoPolo() method. It takes a parameter (int) timesToTest
 *   which you say how many times you want to test.
 *
 */

import java.math.RoundingMode;
import java.text.DecimalFormat;
import java.util.concurrent.ThreadLocalRandom;

public class Box {

    private int no;
    private int numBlack;
    private int numWhite;

    private static int boxCount;

    private static char firstPickedMarble;

    static {
        boxCount = 0;
    }

    private Box( int numBlack, int numWhite ){

        this.numBlack = numBlack;
        this.numWhite = numWhite;

        this.no = ++Box.boxCount;

    }

    private char pickOneMarble( ){

        if( this.numBlack == 0 && this.numWhite>0 ) return 'w';
        if( this.numWhite == 0 && this.numBlack>0 ) return 'b';
        if( this.numWhite == 0 && this.numBlack == 0 ) return 'E';

        int randomInt = ThreadLocalRandom.current().nextInt(1, this.numBlack + this.numWhite +1  );

        return  ( randomInt <= this.numBlack ) ? 'b' : 'w';
    }

    private char carryOneMarbleToAnother( Box otherBox ){

        char pickedMarble = this.pickOneMarble();

        if( pickedMarble == 'b'){
            this.numBlack--;
            otherBox.numBlack++;
        }else{
            this.numWhite--;
            otherBox.numWhite++;
        }

        return pickedMarble;

    }

    private static Box[] createBoxes( int numOfBoxes ){

        Box[] boxes = new Box[5];

        for( int i = 1; i < numOfBoxes; i++){
            boxes[i-1] = new Box( i+1, i+4 );
        }

        return boxes;
    }

    private static void processBoxes( Box[] boxes ){
        for ( int i = 0; i < boxes.length - 1; i++ ){
            char currPickedMarble = boxes[i].carryOneMarbleToAnother( boxes[i+1] );
            if ( i==0 ){
                Box.firstPickedMarble = currPickedMarble;
            }
        }

        for ( int i = boxes.length - 1; 0 < i ; i-- ){
            boxes[i].carryOneMarbleToAnother( boxes[i-1] );
        }
    }

    // For DEBUG purposes only
    static void displayBoxes( Box[] boxes ){

        for ( Box currBox: boxes)
            System.out.format("%d. Box: %d Black, %d White \n", currBox.no, currBox.numBlack, currBox.numWhite);

    }

    private static boolean isFirstPickedB_LastPickedW( Box firstBox ){
        // Pick one marble from first box and check if it white
        // Check if first picked marble is black
        // Both conditions must be true to return true from this function
        return firstBox.pickOneMarble() == 'w' && Box.firstPickedMarble == 'b';
    }

    public static double simulateMarcoPolo( int timesToTest ){

        double timesSucceeded = 0.00;

        for ( int i=0; i<timesToTest; i++ ){

            Box[] boxes;
            boxes = Box.createBoxes( 6 );
            Box.processBoxes( boxes );

            if ( Box.isFirstPickedB_LastPickedW( boxes[0] ) )
                timesSucceeded++;

        }

        return ( timesSucceeded / timesToTest) * 100;
    }

    public static void main(String[] args) {

        DecimalFormat df = new DecimalFormat("#.#####");
        df.setRoundingMode(RoundingMode.CEILING);

        double sim100 = Box.simulateMarcoPolo( 100 );
        double sim1000 = Box.simulateMarcoPolo( 1000 );
        double sim10000 = Box.simulateMarcoPolo( 10000 );

        System.out.format("Marco Polo Simulation Results\n" +
                "Number of Tests: 100, Result: %s%% \n" +
                "Number of Tests: 1000, Result: %s%% \n" +
                "Number of Tests: 10000, Result: %s%% \n"
        , df.format(sim100) , df.format(sim1000), df.format(sim10000));

    }

}
