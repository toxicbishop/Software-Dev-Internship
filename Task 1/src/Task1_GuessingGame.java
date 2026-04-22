package com.internship;

import java.util.Random;
import java.util.Scanner;

public class Task1_GuessingGame {

    private static final Random RANDOM = new Random();
    private static final Scanner SCANNER = new Scanner(System.in);

    private static boolean playRound(int low, int high, int maxAttempts) {
        int secret = RANDOM.nextInt(high - low + 1) + low;
        System.out.printf("%nI picked a number between %d and %d. You have %d attempts.%n",
                low, high, maxAttempts);

        for (int attempt = 1; attempt <= maxAttempts; attempt++) {
            System.out.printf("Attempt %d/%d > ", attempt, maxAttempts);
            String raw = SCANNER.nextLine().trim();

            int guess;
            try {
                guess = Integer.parseInt(raw);
            } catch (NumberFormatException e) {
                System.out.println("Not a valid integer. Try again.");
                continue;
            }

            if (guess < low || guess > high) {
                System.out.printf("Out of range. Stay between %d and %d.%n", low, high);
                continue;
            }

            if (guess == secret) {
                System.out.printf("Correct! Got it in %d attempt(s).%n", attempt);
                return true;
            } else if (guess < secret) {
                System.out.println("Too low.");
            } else {
                System.out.println("Too high.");
            }
        }

        System.out.printf("Out of attempts. Number was %d.%n", secret);
        return false;
    }

    private static int[] pickDifficulty() {
        System.out.println("Difficulty: [1] Easy (1-50, 10 tries)  [2] Medium (1-100, 7 tries)  [3] Hard (1-200, 5 tries)");
        System.out.print("Pick 1/2/3 > ");
        String choice = SCANNER.nextLine().trim();

        switch (choice) {
            case "1": return new int[]{1, 50, 10};
            case "2": return new int[]{1, 100, 7};
            case "3": return new int[]{1, 200, 5};
            default:
                System.out.println("Invalid choice. Defaulting to Medium.");
                return new int[]{1, 100, 7};
        }
    }

    public static void main(String[] args) {
        System.out.println("=== Number Guessing Game ===");
        int wins = 0;
        int losses = 0;

        while (true) {
            int[] settings = pickDifficulty();
            boolean won = playRound(settings[0], settings[1], settings[2]);

            if (won) {
                wins++;
            } else {
                losses++;
            }

            System.out.printf("%nScore: %dW / %dL. Play again? (y/n) > ", wins, losses);
            String again = SCANNER.nextLine().trim().toLowerCase();
            if (!again.equals("y")) {
                System.out.printf("Final score: %d wins, %d losses. Bye.%n", wins, losses);
                break;
            }
        }
    }
}
