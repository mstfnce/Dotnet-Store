(() => {
  const storageKey = "dotnet-store-theme";
  const root = document.documentElement;
  const toggle = document.getElementById("themeToggle");

  const setTheme = (theme) => {
    const isDark = theme === "dark";
    root.classList.toggle("dark-theme", isDark);
    localStorage.setItem(storageKey, theme);

    if (toggle) {
      const icon = toggle.querySelector("i");
      const label = toggle.querySelector("span");
      if (icon) {
        icon.className = isDark ? "fa-solid fa-sun" : "fa-solid fa-moon";
      }
      if (label) {
        label.textContent = isDark ? "Açık" : "Tema";
      }
    }
  };

  const savedTheme = localStorage.getItem(storageKey);
  setTheme(savedTheme === "dark" ? "dark" : "light");

  toggle?.addEventListener("click", () => {
    setTheme(root.classList.contains("dark-theme") ? "light" : "dark");
  });
})();
