import json
import os
import re
import xml.etree.ElementTree as ET
from typing import List, Dict, Optional
import urllib.request

# ----------------------------------------------------------------------------------------------------------------------
# CONFIGURATION - Top level constants you can modify
# ----------------------------------------------------------------------------------------------------------------------
DIRECTORY_PACKAGES_PROPS_PATH = "../Directory.Packages.props"
OUTPUT_JSON_FILE = "../src/InfiniLore.InfiniBlazor/wwwroot/libs/emotes/emotes_lucide.json"
INFINILORE_LUCIDE_PACKAGE_PATTERN = "InfiniLore.Lucide"

UNPKG_ICON_NODES_URL = "https://unpkg.com/lucide-static@latest/icon-nodes.json"
# ----------------------------------------------------------------------------------------------------------------------
# FUNCTIONS
# ----------------------------------------------------------------------------------------------------------------------
def extract_lucide_version_from_props(props_file_path: str) -> Optional[str]:
    try:
        if not os.path.exists(props_file_path):
            print(f"Directory.Packages.props not found at: {props_file_path}")
            return None

        tree = ET.parse(props_file_path)
        root = tree.getroot()

        for item in root.findall(".//PackageVersion"):
            include = item.get("Include", "")
            if INFINILORE_LUCIDE_PACKAGE_PATTERN in include:
                version = item.get("Version", "")
                print(f"Found {include} version: {version}")

                # Format: 0.33.541 -> we want 541
                match = re.search(r'\.(\d+)$', version)
                if match:
                    lucide_version = match.group(1)
                    print(f"Extracted Lucide version: {lucide_version}")
                    return lucide_version

        print(f"No {INFINILORE_LUCIDE_PACKAGE_PATTERN} package found in {props_file_path}")
        return None

    except Exception as e:
        print(f"Error parsing {props_file_path}: {e}")
        return None


def extract_lucide_icons_from_unpkg(lucide_version: Optional[str] = None) -> List[str]:
    if lucide_version:
        url = f"https://unpkg.com/lucide-static@0.{lucide_version}/icon-nodes.json"
        print(f"Attempting to fetch icons for version {lucide_version}")
    else:
        url = UNPKG_ICON_NODES_URL
        print("Fetching latest Lucide icons")

    try:
        print(f"   Fetching from: {url}")

        with urllib.request.urlopen(url) as response:
            data = json.loads(response.read().decode())

        icon_names = list(data.keys()) if isinstance(data, dict) else []

        if icon_names:
            print(f"   Successfully fetched {len(icon_names)} icon names")
            return sorted(icon_names)
        else:
            print(f"   No icons found in response")
            return []

    except Exception as e:
        print(f"   Failed to fetch from UNPKG: {e}")
        return []


def generate_icon_entries(icon_names: List[str]) -> List[Dict]:
    entries = []

    for icon_name in icon_names:
        key_without_dashes = f"li{icon_name.replace('-', '')}"
        key_with_dashes = f"li-{icon_name}"

        keys = [key_without_dashes, key_with_dashes]

        entry = {
            "keys": keys,
            "data": icon_name,
            "contentType": "LucideIconName"
        }
        entries.append(entry)

    return entries



def ensure_output_directory(output_path: str):
    directory = os.path.dirname(output_path)
    if directory and not os.path.exists(directory):
        print(f"   Creating output directory: {directory}")
        os.makedirs(directory, exist_ok=True)


def main():
    print("=" * 80)
    print("Lucide Icons JSON Generator")
    print("=" * 80)

    # Extract version from Directory.Packages.props
    print(f"1. Extracting Lucide version from {DIRECTORY_PACKAGES_PROPS_PATH}...")
    lucide_version = extract_lucide_version_from_props(DIRECTORY_PACKAGES_PROPS_PATH)

    if lucide_version:
        print(f"   Found Lucide version: {lucide_version}")
    else:
        print("   Could not extract version, proceeding with latest")

    # Extract icons from UNPKG
    print("\n2. Extracting icons from UNPKG...")
    icon_names = extract_lucide_icons_from_unpkg(lucide_version)

    if not icon_names:
        print("   Failed to extract icons from UNPKG")
        return

    print(f"   Successfully extracted {len(icon_names)} icons")

    # Generate JSON entries
    print("\n3. Generating JSON entries...")
    entries = generate_icon_entries(icon_names)

    # Ensure output directory exists
    ensure_output_directory(OUTPUT_JSON_FILE)

    # Save to file
    print(f"4. Saving to {OUTPUT_JSON_FILE}...")
    try:
        with open(OUTPUT_JSON_FILE, 'w', encoding='utf-8') as f:
            json.dump(entries, f, indent=2, ensure_ascii=False)

        print(f"   Generated {len(entries)} entries saved to {OUTPUT_JSON_FILE}")

        # Show statistics
        print(f"\n" + "=" * 80)
        print("SUMMARY")
        print("=" * 80)
        print(f"Lucide version targeted: {lucide_version or 'latest'}")
        print(f"Total icons processed: {len(icon_names)}")
        print(f"Output file: {OUTPUT_JSON_FILE}")

        # Show first few examples
        print(f"\nFirst 3 examples:")
        for entry in entries[:3]:
            print(f"  - Keys: {entry['keys']}")
            print(f"    Data: {entry['data']}")
            print()

        # Show some sample icon names
        print(f"Sample icon names: {', '.join(icon_names[:10])}...")

    except Exception as e:
        print(f"   Error saving file: {e}")


if __name__ == "__main__":
    main()
