import UserMenu, { UserMenuProps } from "./UserMenu";
import Navbar, { NavbarProps } from "./Navbar";

export type HeaderProps = {
  navbarProps: NavbarProps;
  userMenuProps: UserMenuProps;
};

export default function Header({ navbarProps, userMenuProps }: HeaderProps) {
  return (
    <header className="sticky top-0 flex h-16 items-center gap-4 border-b bg-background px-4 md:px-6">
      <Navbar menuItems={navbarProps.menuItems} />
      <div className="flex w-full items-center gap-4 md:ml-auto md:gap-2 lg:gap-4">
        <div className="ml-auto flex-1 sm:flex-initial"></div>
        <UserMenu usuario={userMenuProps.usuario} />
      </div>
    </header>
  );
}
