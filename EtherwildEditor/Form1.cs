namespace EtherwildEditor;


public class RuleEditor : Form
{
    private PictureBox tileMapPictureBox;
    // private Bitmap tileMap;
    // private Bitmap annotationLayer;
    private ColorDialog colorDialog;
    private int selectedCurrentTile;
    private int selectedResultingTile;
    private int[] selectedNeighbors;

    public RuleEditor()
    {
        // Initialize components
        tileMapPictureBox = new PictureBox();
        tileMapPictureBox.Dock = DockStyle.Fill;
        tileMapPictureBox.MouseClick += OnTileMapClick;
        Controls.Add(tileMapPictureBox);

        // Load tilemap and initialize annotation layer
        // tileMap = new Bitmap("path_to_tilemap.png");
        // annotationLayer = new Bitmap(tileMap.Width, tileMap.Height);

        // Display tilemap
        // tileMapPictureBox.Image = tileMap;

        // Initialize color dialog and rules list
        colorDialog = new ColorDialog();

        // Initialize selected tiles
        selectedNeighbors = new int[8];
    }

    private void OnTileMapClick(object sender, MouseEventArgs e)
    {
        // Implement tile selection and rule definition logic
        // For example, allow the user to select the current tile, neighbors, and resulting tile
        if (colorDialog.ShowDialog() == DialogResult.OK)
        {
            // For simplicity, assume user selects the current tile color
            selectedCurrentTile = colorDialog.Color.R;

            // Collect neighboring tile colors and resulting tile color in similar way
            // For demonstration, hardcoded values
            selectedNeighbors = new int[] { 1, 0, 1, 0, 0, 1, 0, 1 };
            selectedResultingTile = 2;

            // Encode rule in the fourth row
            // int x = e.X / 32; // Example tile size
            // Color ruleColor = Color.FromArgb(255, selectedCurrentTile, EncodeBitmask(selectedNeighbors), selectedResultingTile);
            // annotationLayer.SetPixel(x, 3, ruleColor);
        }
    }

    private int EncodeBitmask(int[] neighbors)
    {
        int bitmask = 0;
        for (int i = 0; i < 8; i++)
        {
            if (neighbors[i] != 0)
                bitmask |= (1 << i);
        }
        return bitmask;
    }

    private void SaveTilemap(string filePath)
    {
        // Save the modified tilemap with rules encoded
        // tileMap.Save(filePath);
    }

    private void LoadTilemap(string filePath)
    {
        // Load the tilemap
        // tileMap = new Bitmap(filePath);
        // tileMapPictureBox.Image = tileMap;
    }
}