#include <cctype>
#include <iomanip>
#include <iostream>
#include <string>

class TemperatureConverter {
public:
    static double fahrenheitToCelsius(double f) {
        return (f - 32.0) * 5.0 / 9.0;
    }
    static double celsiusToFahrenheit(double c) {
        return c * 9.0 / 5.0 + 32.0;
    }
};

static std::string readLine(const std::string& prompt) {
    std::cout << prompt;
    std::string s;
    std::getline(std::cin, s);
    return s;
}

static double readDouble(const std::string& prompt) {
    while (true) {
        std::string s = readLine(prompt);
        try {
            size_t pos = 0;
            double v = std::stod(s, &pos);
            while (pos < s.size() && std::isspace(static_cast<unsigned char>(s[pos]))) pos++;
            if (pos == s.size()) return v;
        } catch (...) {}
        std::cout << "Not a valid number. Try again.\n";
    }
}

int main() {
    std::cout << "=== Temperature Converter ===\n";
    std::cout << std::fixed << std::setprecision(2);

    while (true) {
        std::cout << "\nDirection:\n"
                  << "  [1] Fahrenheit -> Celsius\n"
                  << "  [2] Celsius -> Fahrenheit\n"
                  << "  [q] Quit\n";
        std::string choice = readLine("Pick > ");
        if (choice == "q" || choice == "Q") {
            std::cout << "Bye.\n";
            break;
        }

        if (choice == "1") {
            double f = readDouble("Temperature in Fahrenheit > ");
            double c = TemperatureConverter::fahrenheitToCelsius(f);
            std::cout << f << " F = " << c << " C\n";
        } else if (choice == "2") {
            double c = readDouble("Temperature in Celsius > ");
            double f = TemperatureConverter::celsiusToFahrenheit(c);
            std::cout << c << " C = " << f << " F\n";
        } else {
            std::cout << "Invalid choice.\n";
        }
    }

    return 0;
}
