//webpack.config.js
const path = require('path');

module.exports = (env, _) => {
    // noinspection JSUnresolvedReference
    const isProduction = env?.production === true || env?.production === 'true';

    return {
        mode: isProduction ? 'production' : 'development',
        devtool: isProduction ? false : 'inline-source-map',
        entry: {
            main: "./src/InfiniBlazor.Core.Js/TypescriptLib/Index.ts",
        },
        output: {
            path: path.resolve(__dirname, './src/InfiniBlazor/wwwroot'),
            filename: "InfiniBlazor.js" // <--- Will be compiled to this single file
        },
        resolve: {
            extensions: [".ts", ".tsx", ".js"],
        },
        module: {
            rules: [
                {
                    test: /\.tsx?$/,
                    loader: "ts-loader",
                    exclude: /node_modules/
                }
            ]
        } 
    }
};