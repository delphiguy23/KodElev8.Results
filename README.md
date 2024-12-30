# KodElev8.Results

## Overview

This project is a C# application that provides a set of extension methods for handling results. It includes methods for checking the status of results, validating results, and chaining operations based on conditions.

## Features

- **Result Status Checks**: Methods to check if a result is successful, created, updated, deleted, not found, failure, or bad request.
- **Conditional Operations**: Methods to perform operations based on conditions.
- **Validation**: Methods to validate results and return appropriate messages.
- **Chaining**: Methods to chain operations based on the success or failure of previous operations.

## Installation

1. Clone the repository:
    ```sh
    git clone https://github.com/delphiguy23/KodElev8.Results.git
    ```
2. Navigate to the project directory:
    ```sh
    cd your-repo-name
    ```
3. Restore the dependencies:
    ```sh
    dotnet restore
    ```

## Usage

### Example

Here is an example of how to use the extension methods provided by this project:

```csharp
using Results.Extension;
using FluentAssertions;

public class Example
{
    public void Run()
    {
        var someValue = 10;

        var result1 = someValue.ToResults();
        var result2 = result1.Is(x => x > 5);
        var result3 = result1.Validate(x => x < 15, "Value must be less than 15");

        result1.IsSuccess().Should().BeTrue();
        result2.IsSuccess().Should().BeTrue();
        result3.IsSuccess().Should().BeTrue();
    }
}
```

## Running Tests

To run the tests, use the following command:

```sh
dotnet test
```

## Contributing

1. Fork the repository.
2. Create a new branch (`git checkout -b feature-branch`).
3. Make your changes.
4. Commit your changes (`git commit -m 'Add some feature'`).
5. Push to the branch (`git push origin feature-branch`).
6. Open a pull request.

## License

This project is licensed under the MIT License. See the `LICENSE` file for more details.

## Contact

For any questions or suggestions, please open an issue or contact the repository owner.

---

**Note**: Replace placeholders like `yourusername` and `your-repo-name` with your actual GitHub username and repository name.
