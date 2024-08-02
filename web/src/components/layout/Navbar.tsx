import Menu, { ItemMenu } from "./Menu";

export type NavbarProps = {
  menuItems: ItemMenu[];
};

export default function Navbar({ menuItems }: NavbarProps) {
  return (
    <nav className="hidden flex-col gap-6 text-lg font-medium md:flex md:flex-row md:items-center md:gap-5 md:text-sm lg:gap-6">
      <Menu items={menuItems} />
    </nav>
  );
}
