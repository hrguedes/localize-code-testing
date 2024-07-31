import { NextResponse } from "next/server";
import type { NextRequest } from "next/server";

export const publicRoutes = [
  "/not-found",
  "/unauthorized",
  "/auth/login",
  "/auth/logout",
];

export const defaultRoutes = [
  "/",
  ""
];

export function middleware(request: NextRequest) {
  if (publicRoutes.includes(request.nextUrl.pathname)) {
    return NextResponse.next();
  } 
  if (defaultRoutes.includes(request.nextUrl.pathname)) {
    var isAuthenticatedToken = request.cookies.get("localize.token")?.value;
    if (isAuthenticatedToken === undefined) {
      request.cookies.clear();
      return NextResponse.redirect(new URL("/auth/logout", request.url));
    }
  }
  return NextResponse.next();
}
