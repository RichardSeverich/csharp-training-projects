const path = require('path');
const HtmlWebpackPlugin = require('html-webpack-plugin');
const MiniCssExtractPlugin = require('mini-css-extract-plugin');

module.exports = {
	entry: './src/index.js',
	output: {
		path: path.resolve(__dirname, 'public'),
		publicPath: '/',
		filename: 'build.js',
	},
	mode: 'development',
	devtool: 'eval-source-map',
	devServer: {
		contentBase: path.resolve(__dirname, './public'),
		historyApiFallback: true,
		publicPath: '/',
		clientLogLevel: 'debug',
		compress: true,
		host: 'localhost',
		port: 4000,
		open: true,
		hot: true,
		overlay: true,
	},
	resolve: {
		extensions: ['.js', '.jsx', '.css'],
		alias: {
			components: path.resolve(__dirname, 'src/components/'),
			context: path.resolve(__dirname, 'src/context/'),
			pages: path.resolve(__dirname, 'src/pages/'),
			helpers: path.resolve(__dirname, 'src/helpers'),
			data: path.resolve(__dirname, 'src/data'),
			api: path.resolve(__dirname, 'src/api'),
		},
	},
	module: {
		rules: [
			{
				test: /\.js$|jsx/,
				exclude: /node_modules/,
				use: ['babel-loader'],
			},
			{
				test: /\.css$/i,
				use: ['style-loader', 'css-loader'],
			},
			{
				test: /\.(png|jpg|gif|svg|eot|ttf|woff|woff2)$/,
				use: [
					{
						loader: 'file-loader',
						options: {
							name: '[name].[ext]',
							useRelativePath: true,
						},
					},
				],
			},
		],
	},
	plugins: [
		new HtmlWebpackPlugin({
			template: './public/index.html',
		}),
		new MiniCssExtractPlugin({
			filename: '[name].css',
			chunkFilename: '[id].css',
		}),
	],
};
