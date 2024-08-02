import Link from "next/link";

export type ItemMenu = {
  label: string;
  url: string;
  active: boolean;
};
export type MenuProps = {
  items: ItemMenu[];
};

export default function Menu({ items }: MenuProps) {
  return (
    <>
      <Link
        href="#"
        className="flex items-center gap-2 text-sm font-bold md:text-base"
      >
        <span>Localize</span>
      </Link>
      {items &&
        items.map((item) => (
          <>
            <Link
              key={item.label}
              href={item.url}
              className={`flex items-center gap-2 text-sm ${
                !item.active ? "font-light" : "font-bold"
              }  md:text-base`}
            >
              <span>{item.label}</span>
            </Link>
          </>
        ))}
    </>
  );
}
