package com.internship;

import java.util.Scanner;

public class Task2_NumberPatterns {

    private static final Scanner SCANNER = new Scanner(System.in);

    private static void rightTriangle(int rows) {
        for (int i = 1; i <= rows; i++) {
            for (int j = 1; j <= i; j++) {
                System.out.print(j + " ");
            }
            System.out.println();
        }
    }

    private static void invertedTriangle(int rows) {
        for (int i = rows; i >= 1; i--) {
            for (int j = 1; j <= i; j++) {
                System.out.print(j + " ");
            }
            System.out.println();
        }
    }

    private static void pyramid(int rows) {
        for (int i = 1; i <= rows; i++) {
            for (int s = 0; s < rows - i; s++) {
                System.out.print("  ");
            }
            for (int j = 1; j <= i; j++) {
                System.out.print(j + " ");
            }
            for (int j = i - 1; j >= 1; j--) {
                System.out.print(j + " ");
            }
            System.out.println();
        }
    }

    private static void floydTriangle(int rows) {
        int n = 1;
        for (int i = 1; i <= rows; i++) {
            for (int j = 1; j <= i; j++) {
                System.out.print(n++ + " ");
            }
            System.out.println();
        }
    }

    private static int readRows() {
        while (true) {
            System.out.print("Enter number of rows (1-20) > ");
            String raw = SCANNER.nextLine().trim();
            try {
                int rows = Integer.parseInt(raw);
                if (rows < 1 || rows > 20) {
                    System.out.println("Out of range. Pick 1-20.");
                    continue;
                }
                return rows;
            } catch (NumberFormatException e) {
                System.out.println("Not a valid integer. Try again.");
            }
        }
    }

    public static void main(String[] args) {
        System.out.println("=== Number Pattern Generator ===");

        while (true) {
            System.out.println();
            System.out.println("Patterns:");
            System.out.println("  [1] Right triangle");
            System.out.println("  [2] Inverted right triangle");
            System.out.println("  [3] Pyramid");
            System.out.println("  [4] Floyd's triangle");
            System.out.println("  [q] Quit");
            System.out.print("Pick a pattern > ");
            String choice = SCANNER.nextLine().trim().toLowerCase();

            if (choice.equals("q")) {
                System.out.println("Bye.");
                break;
            }

            int rows;
            switch (choice) {
                case "1":
                    rows = readRows();
                    System.out.println();
                    rightTriangle(rows);
                    break;
                case "2":
                    rows = readRows();
                    System.out.println();
                    invertedTriangle(rows);
                    break;
                case "3":
                    rows = readRows();
                    System.out.println();
                    pyramid(rows);
                    break;
                case "4":
                    rows = readRows();
                    System.out.println();
                    floydTriangle(rows);
                    break;
                default:
                    System.out.println("Invalid choice.");
            }
        }
    }
}
