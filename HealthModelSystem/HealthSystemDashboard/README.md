# About this app

This is the Health System Dashboard build in reactJS. The app will get the initial health data from the api and renders it in UI. Every updates in health status after that will be received via signalR notifications by connecting to the signalR hub. 

Both rest api base url and api code should be updated in the .env file in the repo's root directory.

so updated it once you clone this repo.


# Required Extension in VScode

Below extensions are required in your editor to ease the deployment to azure.
- Azure CLI Tools 
- Azure App Service
    
## Available Scripts

In the project directory, you can run:

### `yarn start`

Runs the app in the development mode.\
Open [http://localhost:3000](http://localhost:3000) to view it in the browser.

The page will reload if you make edits.\
You will also see any lint errors in the console.

### `yarn build`

Builds the app for production to the `build` folder.\
It correctly bundles React in production mode and optimizes the build for the best performance.

# Deploy

- using azure cli, login and connect to your azure account.
- execute yarn build to get the build files under build folder. 
- right click the build folder and select the deploy option.
- VS code will ask you to choose the existing app service under which this has to be deployed or you can create a new one instead.
- the app will be deployed to that app service and VS Code output window will display the deployment status with the URL.

# React config management 

Note : you can also configure webpack.config.js file by adding externals options
refer below links for more about configuration management in react app

https://www.pluralsight.com/guides/how-to-store-and-read-configuration-files-using-react
https://www.opcito.com/blogs/managing-multiple-environment-configurations-in-react-app