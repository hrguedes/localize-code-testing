/** @type {import('next').NextConfig} */

const nextConfig = {
  output: "standalone",
  reactStrictMode: false,
  env: {
    apiUrl: "http://localhost:8080/api/v1",
  },
};

export default nextConfig;
