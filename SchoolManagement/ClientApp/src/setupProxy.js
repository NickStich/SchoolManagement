const { createProxyMiddleware } = require('http-proxy-middleware');
const { env } = require('process');

// const target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` :
//   env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'http://localhost:61446';
const target = 'https://localhost:7148';

const context =  [
  "/weatherforecast",
  "/student",
  // "/students",
];

module.exports = function(app) {
  console.log('fsdfdsf');
  const appProxy = createProxyMiddleware(context, {
    proxyTimeout: 10000,
    target: target,
    secure: false,
    headers: {
      Connection: 'Keep-Alive'
    }
  });

  app.use(appProxy);
};
