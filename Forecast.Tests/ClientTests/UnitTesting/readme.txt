Steps followed for future reference

http://www.karma-runner.github.io/1.0/intro/installation.html
http://karma-runner.github.io/1.0/intro/configuration.html
http://karma-runner.github.io/1.0/config/configuration-file.html
http://karma-runner.github.io/1.0/config/plugins.html
http://toon.io/how-to-setup-karma-javascript-test-runner/
Node: JavaScript server for runtime
NPM: Node package manager (Nuget for Node)
Karma: Test runner like nUnit
Jasmine - Test case framework like moq

1. Install Node
2. Create package.json with includes for any items you want to download
3. Navigate to the UnitTesting folder here and type NPM install
4. Install Karma client globally to allow run on windows from cmd line - 
npm install -g karma-cli
5. Start karma by simply typing karma

Forecast.Tests\ClientTests\UnitTesting\Specs - add all test scripts here
Forecast.Tests\ClientTests\UnitTesting\TestData - add all test object construction logic here (stubbing etc)
Add subfolders under this folder for individual features

6. Type karma init to configure
a. For framework choose Jasmine
b. For require.js enter no
c. for browsers choose chrome
d. for base path enter ../
e. for source and test files choose Specs/**/*.js which will find all js files in the folder
e. etc
f. Move the generated file to the Config folder

7. Generate a shortcut run command file and place it in the root of the UnitTesting folder to start Karma and pass in the name of the config file

Add karma-chrome-launcher and karma-jasmine to plugins
Set single run to true and concurrency to 1
Add angular, jquery and bootstrap js files to the files array
Add jasmine and angular-mocks to the spec files to permit usage of inject and module