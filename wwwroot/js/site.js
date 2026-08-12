// Scroll reveal via IntersectionObserver
const revealObserver = new IntersectionObserver(
  (entries) => {
    entries.forEach(({ target, isIntersecting }) => {
      if (isIntersecting) {
        target.classList.add('visible');
        revealObserver.unobserve(target);
      }
    });
  },
  { threshold: 0.1 }
);
document.querySelectorAll('.reveal').forEach(el => revealObserver.observe(el));

// Scroll-to-top button
const scrollTopBtn = document.getElementById('scrollTopBtn');
if (scrollTopBtn) {
  window.addEventListener('scroll', () => {
    scrollTopBtn.classList.toggle('visible', window.scrollY > 420);
  }, { passive: true });
  scrollTopBtn.addEventListener('click', () => window.scrollTo({ top: 0, behavior: 'smooth' }));
}

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
        label.textContent = isDark ? "Aydınlık" : "Karanlık";
      }
    }
  };

  const savedTheme = localStorage.getItem(storageKey);
  setTheme(savedTheme === "dark" ? "dark" : "light");

  toggle?.addEventListener("click", () => {
    setTheme(root.classList.contains("dark-theme") ? "light" : "dark");
  });
})();
